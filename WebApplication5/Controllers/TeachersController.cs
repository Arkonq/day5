using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Data;
using WebApplication5.Models;
using WebApplication5.ViewModels;

namespace WebApplication5.Controllers
{
	public class TeachersController : Controller
	{
		private ApplicationDataContext db = new ApplicationDataContext();

		// GET: Teachers
		public ActionResult Index()
		{
			return View(db.Teachers.ToList());
		}

		// GET: Teachers/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Teacher teacher = db.Teachers.Include(t => t.Subject).FirstOrDefault(t => t.Id == id);
			if (teacher == null)
			{
				return HttpNotFound();
			}
			return View(teacher);
		}

		// GET: Teachers/Create
		public ActionResult Create()
		{
			ViewBag.Subjects = db.Subjects.ToList();
			return View();
		}

		// POST: Teachers/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(TeacherViewModel teacherViewModel)
		{
			if (ModelState.IsValid)
			{
				Subject subject = db.Subjects.Find(teacherViewModel.SubjectId);
				Teacher teacher = new Teacher()
				{
					Name = teacherViewModel.Name,
					Subject = subject
				};

				db.Teachers.Add(teacher);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(teacherViewModel);
		}

		// GET: Teachers/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Teacher teacher = db.Teachers.Find(id);

			if (teacher == null)
			{
				return HttpNotFound();
			}

			var viewModel = new TeacherViewModel()
			{
				Id = teacher.Id,
				Name = teacher.Name,
				Subjects = db.Subjects.ToList()
			};

			return View(viewModel);
		}

		// POST: Teachers/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(TeacherViewModel teacherViewModel)
		{
			if (ModelState.IsValid)
			{
				var teacher = db.Teachers.Find(teacherViewModel.Id);
				teacher.Name = teacherViewModel.Name;
				teacher.Subject = db.Subjects.Find(teacherViewModel.SubjectId);
				db.Entry(teacher).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(teacherViewModel);
		}

		// GET: Teachers/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Teacher teacher = db.Teachers.Include(t => t.Subject).FirstOrDefault(t => t.Id == id);
			if (teacher == null)
			{
				return HttpNotFound();
			}
			return View(teacher);
		}

		// POST: Teachers/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Teacher teacher = db.Teachers.Find(id);
			db.Teachers.Remove(teacher);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
