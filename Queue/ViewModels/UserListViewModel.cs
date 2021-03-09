using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Queue.Models;

namespace Queue.ViewModels
{
    public class UserListViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Usename")]
        public string Email { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public Boolean IsLoged { get; set; }

        public string Role { get; set; }

        public UserListViewModel(ApplicationUser user, string role)
        {
            this.Id = user.Id;
            this.Email = user.Email;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.IsLoged = user.IsLoged;
            this.Role = role;
        }

    }
}