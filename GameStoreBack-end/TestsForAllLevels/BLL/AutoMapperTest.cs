using AutoMapper;
using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsForAllLevels.BLL
{
    [TestFixture]
    public class AutoMapperTestBLL
    {
        [Test]
        public void AutomapperConfiguration_ReturnsIsVaid()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<AutoMapperProfile>();
            });

            config.AssertConfigurationIsValid();    
        }
    }
}
