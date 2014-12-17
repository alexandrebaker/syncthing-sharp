using System;
using System.Diagnostics;
using System.Text;
using NUnit.Framework;
using Syncthing.Protocol.v1;

namespace Syncthing.Tests.Protocol
{
    [TestFixture]
    public class DeviceIdTest
    {
        private readonly string _formatted = "P56IOI7-MZJNU2Y-IQGDREY-DM2MGTI-MGL3BXN-PQ6W5BM-TBBZ4TJ-XZWICQ2";
        private readonly string[] _formatCases =
            {
            "P56IOI-7MZJNU-2IQGDR-EYDM2M-GTMGL3-BXNPQ6-W5BTBB-Z4TJXZ-WICQ",
            "P56IOI-7MZJNU2Y-IQGDR-EYDM2M-GTI-MGL3-BXNPQ6-W5BM-TBB-Z4TJXZ-WICQ2",
            "P56IOI7 MZJNU2I QGDREYD M2MGTMGL 3BXNPQ6W 5BTB BZ4T JXZWICQ",
            "P56IOI7 MZJNU2Y IQGDREY DM2MGTI MGL3BXN PQ6W5BM TBBZ4TJ XZWICQ2",
            "P56IOI7MZJNU2IQGDREYDM2MGTMGL3BXNPQ6W5BTBBZ4TJXZWICQ",
            "p56ioi7mzjnu2iqgdreydm2mgtmgl3bxnpq6w5btbbz4tjxzwicq",
            "P56IOI7MZJNU2YIQGDREYDM2MGTIMGL3BXNPQ6W5BMTBBZ4TJXZWICQ2",
            "P561017MZJNU2YIQGDREYDM2MGTIMGL3BXNPQ6W5BMT88Z4TJXZWICQ2",
            "p56ioi7mzjnu2yiqgdreydm2mgtimgl3bxnpq6w5bmtbbz4tjxzwicq2",
            "p561017mzjnu2yiqgdreydm2mgtimgl3bxnpq6w5bmt88z4tjxzwicq2",
        };


        [Test]
        public void TestFormatDeviceId()
        {
            foreach (var @case in _formatCases)
            {
                var device = DeviceId.LocalDeviceId;
                device.UnMarshalText(Encoding.UTF8.GetBytes(@case));
                var d = device.ToString();

                Assert.AreEqual(d, _formatted, String.Format("Format Device Id ({0}) : \n\t{1} != {2} ", @case, d, _formatted));
            }
        }

        private readonly Tuple<string, bool>[] _validCases =
            {
            new Tuple<string, bool>("", false),
            new Tuple<string, bool>("P56IOI7-MZJNU2Y-IQGDREY-DM2MGTI-MGL3BXN-PQ6W5BM-TBBZ4TJ-XZWICQ2", true),
            new Tuple<string, bool>("P56IOI7-MZJNU2-IQGDREY-DM2MGT-MGL3BXN-PQ6W5B-TBBZ4TJ-XZWICQ", true),
	        new Tuple<string, bool>("P56IOI7 MZJNU2I QGDREYD M2MGTMGL 3BXNPQ6W 5BTB BZ4T JXZWICQ", true),
	        new Tuple<string, bool>("P56IOI7MZJNU2IQGDREYDM2MGTMGL3BXNPQ6W5BTBBZ4TJXZWICQ", true),
	        new Tuple<string, bool>("P56IOI7MZJNU2IQGDREYDM2MGTMGL3BXNPQ6W5BTBBZ4TJXZWICQCCCC", false),
	        new Tuple<string, bool>("p56ioi7mzjnu2iqgdreydm2mgtmgl3bxnpq6w5btbbz4tjxzwicq", true),
	        new Tuple<string, bool>("p56ioi7mzjnu2iqgdreydm2mgtmgl3bxnpq6w5btbbz4tjxzwicqCCCC", false)
        };

        [Test]
        public void TestValidateDeviceId()
        {
            int count = 0;
            foreach (var @case in _validCases)
            {
                var device = DeviceId.LocalDeviceId;
                var haveError = false;
                try
                {
                    device.UnMarshalText(Encoding.UTF8.GetBytes(@case.Item1));
                }
                catch (Exception)
                {
                    haveError = true;
                }

                if (!haveError && !@case.Item2 || haveError && @case.Item2)
                    Assert.Fail("#{3} - ValidateDeviceID({0}); {1} != {2}", @case.Item1, haveError, !@case.Item2, count);

                count++;
            }
        }

        [Test]
        public void TestMarshallingDeviceId()
        {
            var n0 = new DeviceId(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 10, 21, 22, 23, 24,
                25, 26, 27, 28, 29, 30, 31, 32);
            var n1 = DeviceId.LocalDeviceId;
            var n2 = DeviceId.LocalDeviceId;

            var bs = n0.MarshalText();
            n1.UnMarshalText(bs);

            bs = n1.MarshalText();
            n2.UnMarshalText(bs);

            Assert.AreEqual(n2.ToString(),n0.ToString(), string.Format("String marshalling error; {0} != {1}", n2.ToString(), n0.ToString()));
            Assert.IsTrue(n2.Equals(n0), "Equals error");
            Assert.IsTrue(n2.CompareTo(n0) == 0, "Compare error");
        }
    }
}
