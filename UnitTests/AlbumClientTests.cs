using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class AlbumClientTests
    {
        // TODO:
        //   mock client here to use in tests

        [Fact(Skip ="TODO")]
        public async Task Get_ValidAlbumId()
        {
            // fake client returns a valid list 
            // test that the Get returns the expected response, as setup above
        }

        [Fact(Skip = "TODO")]
        public async Task Get_InvalidAlbumId()
        {
            // fake client returns an empty list 
            // test that the Get returns an empty list, as setup above
        }
    }
}
