using DGSW_POS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DGSW_POS.View;

namespace DGSW_POS.Controller
{
    //메인 폼과 통계 폼 사이의 컨트롤러
    public class BForm
    {
        private static BModel bm;
        private Main main;
        private Chart chart;
        
        public void initmain(Main m)
        {
            this.main = m;
        }

        public void showchart()
        {
            main.Visible = false;
            bm = AForm.bm; //메인-주문 컨트롤러에서 저장한 구매 데이터를 메인-통계 컨트롤러로 이동
            chart = new Chart();
            chart.Show();
        }

        public void backmain()
        {
            main.Visible = true;
        }

        public void setcategoryData(int [] categoryct, int [] categoryprice) //기본 데이터 추가
        {
            for (int i = 0; i < bm.BuyList.Count; i++)
            {
                switch (bm.BuyList[i].FKind)
                {
                    case "김밥류":
                        categoryct[0] += bm.BuyList[i].FCount;
                        categoryprice[0] += bm.BuyList[i].FPrice;
                        break;
                    case "식사류":
                        categoryct[1] += bm.BuyList[i].FCount;
                        categoryprice[1] += bm.BuyList[i].FPrice;
                        break;
                    case "분식류":
                        categoryct[2] += bm.BuyList[i].FCount;
                        categoryprice[2] += bm.BuyList[i].FPrice;
                        break;
                    case "음료수":
                        categoryct[3] += bm.BuyList[i].FCount;
                        categoryprice[3] += bm.BuyList[i].FPrice;
                        break;
                }
            }
        }

        public void addmenuchartData(List<CMenuModel>kindList, List<string>foodname) //기본 데이터 추가
        {
            for (int i = 0; i < bm.BuyList.Count; i++)
            {
                foodname.Add(bm.BuyList[i].FName); //음식이름 추가
                CMenuModel cm = new CMenuModel();
                cm.FName = bm.BuyList[i].FName;
                cm.FCount = bm.BuyList[i].FCount;
                cm.FPrice = bm.BuyList[i].FPrice;
                kindList.Add(cm); //리스트 추가
            }
        }

        public string setmoneycardData(int all, int card, int money)
        {
            for (int i = 0; i < bm.BuyList.Count; i++)
            {
                all += bm.BuyList[i].FPrice;
                if (bm.BuyList[i].FBuy.Equals("카드"))
                {
                    card += bm.BuyList[i].FPrice;
                }
                else
                {
                    money += bm.BuyList[i].FPrice;
                }
            }
            string data = "현금 : " + money + "원\n카드 : " + card + "원\n합계 : " + all + "원";
            return data;
        }
    }
}
