using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContactsList.MVC.Models;
using ContactsList.MVC;

namespace CompanyContacts.API.Controllers
{
    public class ContactsController : Controller
    {
        private ContactsListAPIPreet Api = new ContactsListAPIPreet();
        //private ContactsListAPIPreet Api = new ContactsListAPIPreet(new Uri("http://localhost:51864"));

        // GET: Contacts
        public ActionResult Index()
        {
            return View(Api.Contacts.Get());
        }

        // GET: Contacts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = Api.Contacts.Get().Single(c => c.Id == id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Contacts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,EmailAddress")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                contact.Id = Api.Contacts.Get().OrderByDescending(c => c.Id).First().Id + 1;
                Api.Contacts.Post(contact);
                return RedirectToAction("Index");
            }

            return View(contact);
        }

        // GET: Contacts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = Api.Contacts.Get().Single(c => c.Id == id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,EmailAddress")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                Api.Contacts.Delete(contact.Id.Value);
                Api.Contacts.Post(contact);
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = Api.Contacts.Get().Single(c => c.Id == id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Api.Contacts.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Api.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
