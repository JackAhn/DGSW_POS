using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGSW_POS.Model
{
    //손님이 구매한 데이터를 저장하여 차트에서 사용하기 위한 모델
    public class BModel
    {
        public List<FInfor> BuyList = new List<FInfor>();
    }
}
