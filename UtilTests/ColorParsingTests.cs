using Microsoft.VisualStudio.TestTools.UnitTesting;
using LightBlueFox.Util.Types;

namespace UtilTests
{
    [TestClass]
    public class ColorParsingTests
    {
        [TestMethod]
        public void normalHexCode()
        {
            int i = 0;
            string[] nofail = { "#afafaf", "#0F4CDD", "#0f4CFf", "#af442256", "0xdfcf23", "0xDD43fC", "0xDD21CD54" };
            string[] fail = { "#afsfdf", "#0f33454", "#afafafsd", "#0456123", "F0x32fc45", "00xdfddff", "0xdfdfd", "0x1234567" };
            Color c;
            try
            {
                for (i = 0; i < nofail.Length; i++)
                {
                    c = new Color(nofail[i]);
                }
            }
            catch (System.Exception)
            {
                Assert.Fail("There was an exception thrown when trying to convert string " + nofail[i] + " to a color");
            }

            try
            {
                for (i = 0; i < fail.Length; i++)
                {
                    c = new Color(fail[i]);
                    Assert.Fail("Color created from invalid string: " + fail[i] + " converts to color " + c);
                }
            }
            catch (System.Exception)
            {
            }
        }
    }
}