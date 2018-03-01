using AutoMapper;
using System;

namespace NexPay.BankApp.Core.Automapper.Profiles
{
    public class AppProfile: Profile
    {
        public AppProfile()
        {
            CreateMap<Model.PaymentDetails, ViewModel.PaymentDetails>().ReverseMap();
        }
    }
}
