using AutoMapper;
using WebApi;

namespace TestsForAllLevels
{
    public class Tests
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
}