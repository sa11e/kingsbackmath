using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KingsBackMath.Data.Entities;
using KingsBackMath.ViewModels;

namespace KingsBackMath.Data
{
    public class KingsBackMathMappingProfile : Profile
    {
        public KingsBackMathMappingProfile()
        {
            CreateMap<Game, GameViewModel>()
                .ReverseMap();
        }
    }
}
