using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FPTBook_System.Controllers;
using FPTBook_System.Data;
using Microsoft.Data.SqlClient;

namespace FPTBook_System.Models
{
    public class useraccountsController : Controller
    {
        private readonly FPTBook_SystemContext _context;

        public useraccountsController(FPTBook_SystemContext context)
        {
            _context = context;
        }

        // GET: useraccounts
        public async Task<IActionResult> Index()
        {
              return _context.useraccounts != null ? 
                          View(await _context.useraccounts.ToListAsync()) :
                          Problem("Entity set 'FPTBook_SystemContext.useraccounts'  is null.");
        }

       
        
        // GET: useraccounts/Create
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        //Login post action 
        [HttpPost, ActionName("login")]
        [ValidateAntiForgeryToken]
        public IActionResult login(string na, string pa)
        {
            SqlConnection conn1 = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\super\\OneDrive\\Documents\\asm2database.mdf;Integrated Security=True;Connect Timeout=30");
            string sql;
            sql = "SELECT * FROM usersaccounts where name ='" + na + "' and  pass ='" + pa + "' ";
            SqlCommand comm = new SqlCommand(sql, conn1);
            conn1.Open();
            SqlDataReader reader = comm.ExecuteReader();

            if (reader.Read())
            {
                string role = (string)reader["role"];
                string id = Convert.ToString((int)reader["Id"]);
                HttpContext.Session.SetString("Name", na);
                HttpContext.Session.SetString("Role", role);
                HttpContext.Session.SetString("userid", id);
                reader.Close();
                conn1.Close();
                if (role == "customer")
                    return RedirectToAction("catalogue", "books");

                else
                    return RedirectToAction("Index", "books");

            }
            else
            {
                ViewData["Message"] = "wrong user name password";
                return View();
            }
        }



        // POST: useraccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,name,pass,email")] useraccounts useraccounts)
        {
            useraccounts.role = "customer";
                _context.Add(useraccounts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(login));
            
        }

        public Task<IActionResult> Edit()
        {
            return Edit(_context);
        }

        // GET: useraccounts/Edit/5
        public async Task<IActionResult> Edit(FPTBook_SystemContext _context)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetString("userid"));
            var useraccounts = await _context.useraccounts.FindAsync(id);
            
            return View(useraccounts);
        }

        // POST: useraccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,name,pass,role,email")] useraccounts useraccounts)
        {

            _context.Update(useraccounts);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(login));
        }
        private bool useraccountsExists(int id)
        {
          return (_context.useraccounts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
