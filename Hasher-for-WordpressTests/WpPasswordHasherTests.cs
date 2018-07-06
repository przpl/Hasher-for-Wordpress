using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WordpressPasswordHasher;

namespace Hasher_for_WordpressTests
{
    [TestClass]
    public class WpPasswordHasherTests
    {
        WpPasswordHasher _hasher = new WpPasswordHasher();

        #region Verify()
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Verify_HashIsNull_ArgumentNullException()
        {
            _hasher.Verify(null, "test");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Verify_PasswordIsNull_ArgumentNullException()
        {
            _hasher.Verify("$P$B55D6LjfH", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Verify_HashLengthIsLessThan12_ArgumentNullException()
        {
            _hasher.Verify("12345678901", "test");
        }

        [TestMethod]
        public void Verify_ValidHashes_True()
        {
            Assert.IsTrue(_hasher.Verify("$P$B55D6LjfHDkINU5wF.v2BuuzO0/XPk/", "test"));
            Assert.IsTrue(_hasher.Verify("$P$BHCABn.UYdpZe4vDx5ySczzaOlf4yG/", "passw0rd"));
            Assert.IsTrue(_hasher.Verify("$P$BLkk8sG3aPK1t8PuxlIByoCoeQdvy/1", "UYdpZe4vDx5ySczzaOlf4yG"));
        }

        [TestMethod]
        public void Verify_InvalidHashes_False()
        {
            Assert.IsFalse(_hasher.Verify("$P$B55D6LjfHDkINU5wF.v2BuuzO0/XPk/", "test2"));
            Assert.IsFalse(_hasher.Verify("$P$BHCABn.UYdpZe4vDx5ySczzaOlf4yG/", "passw0rd2"));
            Assert.IsFalse(_hasher.Verify("$P$BLkk8sG3aPK1t8PuxlIByoCoeQdvy/1", "UYdpZe4vDx5ySczzaOlf4yG2"));
        }
        #endregion

        #region Hash
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Hash_PasswordIsNull_ArgumentNullException()
        {
            _hasher.Hash(null, "123456789012");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Hash_SaltIsNull_ArgumentNullException()
        {
            _hasher.Hash("test", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Hash_SaltLengthIsNot12Characters_ArgumentNullException()
        {
            _hasher.Hash("test", "1234567890123");
        }

        [TestMethod]
        public void Hash()
        {
            Assert.AreEqual("$P$B55D6LjfHDkINU5wF.v2BuuzO0/XPk/", _hasher.Hash("test", "$P$B55D6LjfH"));
            Assert.AreEqual("$P$BHCABn.UYdpZe4vDx5ySczzaOlf4yG/", _hasher.Hash("passw0rd", "$P$BHCABn.UY"));
            Assert.AreEqual("$P$BLkk8sG3aPK1t8PuxlIByoCoeQdvy/1", _hasher.Hash("UYdpZe4vDx5ySczzaOlf4yG", "$P$BLkk8sG3a"));
        }
        #endregion
    }
}
