using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investimento.RendaFixa.Test.Entities
{
    [TestClass]
    public class RendaFixaTest
    {
        [TestMethod]
        public void ShouldCalculateIROfRendaFixaWhenDataIsValid()
        {
            var rendaFixa = new Domain.Entities.RendaFixa(2000.0, 2097.85, 2.0, DateTime.Parse("2021-03-09T00:00:00"), 0.0, 0.0, 0.0, "97% do CDI", "LCI", "BANCO MAXIMA", true, DateTime.Parse("2019-03-14T00:00:00"), 1048.927450, false);

            Assert.IsTrue(rendaFixa.IR == 4.8924999999999956);
        }

        [TestMethod]
        public void ShouldNotCalculateIROfRendaFixaWhenDataIsInvalid()
        {
            var rendaFixa = new Domain.Entities.RendaFixa(0, 0, 2.0, DateTime.Parse("2021-03-09T00:00:00"), 0.0, 0.0, 0.0, "97% do CDI", "LCI", "BANCO MAXIMA", true, DateTime.Parse("2019-03-14T00:00:00"), 1048.927450, false);

            Assert.IsTrue(rendaFixa.IR == 0);
        }

        [TestMethod]
        public void ShouldCalculateResgateLessThan3MonthsExpirationDateofRendaFixaWhenDataIsValid()
        {
            var rendaFixa = new Domain.Entities.RendaFixa(2000.0, 2097.85, 2.0, DateTime.Now.AddMonths(3), 0.0, 0.0, 0.0, "97% do CDI", "LCI", "BANCO MAXIMA", true, DateTime.Now.AddMonths(-3), 1048.927450, false);
            Assert.IsTrue(rendaFixa.ValorResgate == 1971.9789999999998);
        }

        [TestMethod]
        public void ShouldCalculateResgateMoreThanHalfExpirationDateofRendaFixaWhenDataIsValid()
        {
            var rendaFixa = new Domain.Entities.RendaFixa(2000.0, 2097.85, 2.0, DateTime.Now.AddMonths(5), 0.0, 0.0, 0.0, "97% do CDI", "LCI", "BANCO MAXIMA", true, DateTime.Now.AddMonths(-7), 1048.927450, false);
            Assert.IsTrue(rendaFixa.ValorResgate == 1783.1725);
        }

        [TestMethod]
        public void ShouldCalculateResgateLessThanHalfExpirationDateofRendaFixaWhenDataIsValid()
        {
            var rendaFixa = new Domain.Entities.RendaFixa(2000.0, 2097.85, 2.0, DateTime.Now.AddMonths(12), 0.0, 0.0, 0.0, "97% do CDI", "LCI", "BANCO MAXIMA", true, DateTime.Now.AddMonths(-7), 1048.927450, false);
            Assert.IsTrue(rendaFixa.ValorResgate == 1468.495);
        }
    }
}
