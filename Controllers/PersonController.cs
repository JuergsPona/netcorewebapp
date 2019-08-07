using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FamilyTree.Models;

namespace FamilyTree.Controllers
{
    public class PersonController : Controller
    {
        private readonly familytreedbContext _context;

        public PersonController(familytreedbContext context)
        {
            _context = context;
        }

        // GET: Person
        public async Task<IActionResult> Index()
        {
            List<Person> list = new List<Person>();
            Person person = new Person();
            list = await _context.People.Where<Person>(x => x.Mother != null || x.Father != null).ToListAsync();
            ViewBag.selectPerson = person;
            ViewBag.listofitems = list;
            return View();
        }

        // GET: Person/Details/5
        public async Task<IActionResult> Details(Person selectedPerson, int id = 0)
        {
            PersonViewModel person = new PersonViewModel();

            if (selectedPerson.PersonId == 0 && id == 0)
            {
                return NotFound();
            }
            else if (selectedPerson.PersonId == 0 && id != 0)
            {
                person = await GetDataForPerson(id);
                return View(person);
            }
            else if (selectedPerson.PersonId != 0)
            {
                person = await GetDataForPerson(selectedPerson.PersonId);
                return View(person);
            }
            else
            {
                return NotFound();
            }
        }

        private async Task<PersonViewModel> GetDataForPerson(int id)
        {
            var personObject = await _context.People
                .FirstOrDefaultAsync(m => m.PersonId == id);
            if (personObject == null)
            {
                return null;
            }

            PersonViewModel person = new PersonViewModel();
            person.person = personObject;
            person.parents = new List<Person>();
            person.parents.Add(await _context.People
                .FirstOrDefaultAsync(m => m.FullName == person.person.Mother));
            person.parents.Add(await _context.People
                .FirstOrDefaultAsync(m => m.FullName == person.person.Father));

            return person;
        }


        // GET: Person/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Person/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,FullName,IsDeceased,Mother,Father,IsParent")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: Person/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: Person/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonId,FullName,IsDeceased,Mother,Father,IsParent")] Person person)
        {
            if (id != person.PersonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.PersonId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: Person/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .FirstOrDefaultAsync(m => m.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.People.FindAsync(id);
            _context.People.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return _context.People.Any(e => e.PersonId == id);
        }

    }
}
