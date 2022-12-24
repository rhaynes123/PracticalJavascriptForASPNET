using System;
using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using RocketBank.DTOs;
using RocketBank.Models;

namespace RocketBank.Mappers
{
    #region
    /*
     * https://learn.microsoft.com/en-us/aspnet/core/security/data-protection/using-data-protection?view=aspnetcore-7.0
     * https://code-maze.com/data-protection-aspnet-core/
     * https://andrewlock.net/an-introduction-to-the-data-protection-system-in-asp-net-core/
     * https://medium.com/swlh/how-to-distribute-data-protection-keys-with-an-asp-net-core-web-app-8b2b5d52851b
     */
    #endregion
    public sealed class CustomerProfile: Profile
	{
		public CustomerProfile()
		{
            MapAndProtectCustomers();
        }
        private void MapAndProtectCustomers()
        {
            IDataProtectionProvider provider = DataProtectionProvider.Create("CustomerApp");
            IDataProtector _protector = provider.CreateProtector(purpose: "CustomerInfoProtection");
            CreateMap<CustomerDto, Customer>()
                .ForMember(destination => destination.SocialSecurityNumber, opt => opt.MapFrom(source => _protector.Protect(source.SocialSecurityNumber)))
                .ForMember(destination => destination.AccountingNumber, opt => opt.MapFrom(source => _protector.Protect(source.AccountingNumber)))
                .ForMember(destination => destination.RoutingNumber, opt => opt.MapFrom(source => _protector.Protect(source.RoutingNumber)));

            CreateMap<Customer, CustomerDto>()
                .ForMember(destination => destination.SocialSecurityNumber, opt => opt.MapFrom(source => _protector.Unprotect(source.SocialSecurityNumber)))
                .ForMember(destination => destination.AccountingNumber, opt => opt.MapFrom(source => _protector.Unprotect(source.AccountingNumber)))
                .ForMember(destination => destination.RoutingNumber, opt => opt.MapFrom(source => _protector.Unprotect(source.RoutingNumber)));


        }
    }
}

