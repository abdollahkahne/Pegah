using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCSample.Data;
using MVCSample.Models;

namespace MVCSample.Services
{
    public class PeopleService
    {
        private readonly ILogger<PeopleService> _logger;
        private readonly PeopleDbContext _context;

        public PeopleService(ILogger<PeopleService> logger, PeopleDbContext context)
        {
            _logger = logger;
            _context = context;
            // Create Database if it is not created yet (code first)
            _context.Database.EnsureCreated();
        }
        public Person? GetPerson(long id)
        {
            return _context.People.Include(p => p.Educations).ThenInclude(e => e.University).SingleOrDefault(p => p.Id == id);
        }
        public IQueryable<Person> GetPeople(string? start = null, string? finish = null, string? fullname = null, string? nationalId = null)
        {
            var query = _context.People.AsQueryable();
            if (start is not null)
            {
                var startDate = PersianDateParser.ParseUsingCulture(start);
                query = query.Where(p => p.Educations.Any(e => e.Start >= startDate!)).AsQueryable();
            }
            if (finish is not null)
            {
                var finishDate = PersianDateParser.ParseUsingCulture(finish);
                query = query.Where(p => p.Educations.Any(e => e.End <= finishDate!)).AsQueryable();
            }
            if (!string.IsNullOrEmpty(fullname))
            {
                query = query.Where(p => string.Concat(p.FirstName, " ", p.LastName).Contains(fullname)).AsQueryable();
            }
            if (!string.IsNullOrEmpty(nationalId))
            {
                query = query.Where(p => p.NationalId == nationalId).AsQueryable();
            }
            return query;
        }
        public async Task<int> RegisterPeople(PersonViewModel data)
        {
            Person p = new Person()
            {
                FirstName = data.FirstName,
                LastName = data.LastName,
                NationalId = data.NationalId,

            };
            _context.People.Add(p);
            var educations = data.UniversityLines.Select(line => new Education()
            {
                UniversityId = line.UniversityId,
                Level = line.Level,
                GPA = line.GPA,
                Major = line.Major,
                Minor = line.Minor,
                Start = PersianDateParser.ParseUsingCulture(line.StartDate),
                End = PersianDateParser.ParseUsingCulture(line.EndDate),
                Person = p,
            });
            _context.Educations.AddRange(educations);
            return await _context.SaveChangesAsync();
        }
        public IQueryable<University> GetUniversities()
        {
            return _context.Universities.OrderBy(u => u.Name);
        }
        public async Task<int> AddUniversity(UniversityViewModel dto)
        {
            _context.Universities.Add(new University()
            {
                Name = dto.Name,
                Type = dto.Type,
                Address = dto.Address,
            });
            return await _context.SaveChangesAsync();
        }

    }
}