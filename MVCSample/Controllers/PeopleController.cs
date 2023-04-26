using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCSample.Models;
using MVCSample.Data;
using MVCSample.Services;

namespace MVCSample.Controllers
{
    public class PeopleController : Controller
    {
        private readonly ILogger<PeopleController> _logger;
        private readonly PeopleService _peopleService;

        public PeopleController(ILogger<PeopleController> logger, PeopleService peopleService)
        {
            _logger = logger;
            _peopleService = peopleService;
        }

        public IActionResult Index(string? start = null, string? finish = null, string? fullname = null, string? nationalId = null)
        {
            var people = _peopleService.GetPeople(start, finish, fullname, nationalId)
            .Select(p => new PersonViewModel()
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                NationalId = p.NationalId,
                Created = p.Created,
            }).AsEnumerable();
            var model = new IndexViewModel()
            {
                Start = start,
                Finish = finish,
                Fullname = fullname,
                NationalId = nationalId,
                People = people,
            };
            return View(model);
        }
        public IActionResult Register()
        {
            return View();
        }
        public async Task<IActionResult> RegisterMe(PersonViewModel data)
        {

            await _peopleService.RegisterPeople(data);
            return Ok();
        }
        public JsonResult GetUniversities()
        {
            var universities = _peopleService.GetUniversities().AsEnumerable();
            return Json(universities);
        }
        public IActionResult Display(long id)
        {

            var p = _peopleService.GetPerson(id);
            PersonViewModel? model = null;
            if (p is not null)
            {
                model = new PersonViewModel()
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    NationalId = p.NationalId ?? "",
                    Created = p.Created,
                    UniversityLines = p.Educations.Select(e => new UniversityLine()
                    {
                        University = new UniversityViewModel()
                        {
                            Type = e.University.Type,
                            Name = e.University.Name,
                            Address = e.University.Address,
                        },
                        UniversityId = e.University.Id,
                        Level = e.Level,
                        Major = e.Major,
                        Minor = e.Minor,
                        GPA = e.GPA ?? default,
                        StartDate = PersianDateParser.FormatUsingCulture(e.Start),
                        EndDate = PersianDateParser.FormatUsingCulture(e.End),
                    }).AsEnumerable(),
                };
                return View(model);
            }
            else
            {
                return Redirect("/people");

            }

        }
        public async Task<IActionResult> AddUniversity(UniversityViewModel university)
        {
            await _peopleService.AddUniversity(university);
            return Ok();
        }


    }
}