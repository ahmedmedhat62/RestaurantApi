using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RestaurantApi.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RestaurantApi.Auth
{
      public class User1 : IdentityUser<Guid>, IBaseEntity
    {

        public string Address { get; set; }
        public string PhoneNumber {get; set; }
        public Gender Gender { get; set; }

        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public DateTime? DeleteDate { get; set; }
        //Guid IBaseEntity.Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //DateTime IBaseEntity.CreateDateTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //DateTime IBaseEntity.ModifyDateTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //DateTime? IBaseEntity.DeleteDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        // public DateTime CreateDateTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //public DateTime ModifyDateTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //public DateTime? DeleteDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
