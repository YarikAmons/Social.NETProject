using AutoMapper;
using Social.NetWork.BLL.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Social.NetWork.WEB.App_Start {
    public class AutomapperConfiguration {
        public static IMapper Configure() {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<DomainModelToDtoProfile>();
                cfg.AddProfile<DtoToViewModelProfile>();
            });
            return config.CreateMapper();
        }
    }
}