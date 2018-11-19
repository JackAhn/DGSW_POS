using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using DGSW_POS.View;
using DGSW_POS.Model;
using System.IO;

namespace DGSW_POS.Controller
{
    //메인과 주문 폼에서 사용되는 컨트롤러
    public class AForm
    {
        public static FModel[] fm = new FModel[8]; //정적 테이블 별 데이터 저장 객체 배열 생성
        public static BModel bm = new BModel(); //정적 음식 구입 데이터 저장 객체 생성
        private Main main; //메인 폼 생성을 위한 변수
        private Menulist list; //주문 폼 생성을 위한 변수  
        private int Tableno; //선택한 테이블 번호

        public AForm()
        {
            
        }

        public void initmain(Main m) //메인 데이터 초기화
        {
            main = m; //생성한 메인 객체 전역변수에 삽입
        }
        public void showmenu(int btn) //주문 폼 활성화
        {
            main.Visible = false;
            Tableno = btn;
            list = new Menulist(btn);
            list.Show();
  
        }

        public void backmenu() //다시 메인으로
        {
            main.Visible = true;
        }

        public void insertdata(ListView listview1) //주문한 데이터 넣기
        {
            fm[Tableno - 1].TData.Clear(); //원래 들어간 데이터 초기화
            for (int i = 0; i < listview1.Items.Count; i++)
            {
                string name = listview1.Items[i].SubItems[0].Text;
                int count = int.Parse(listview1.Items[i].SubItems[1].Text);
                int price = int.Parse(listview1.Items[i].SubItems[2].Text);
                string kind = listview1.Items[i].SubItems[3].Text;
                fm[Tableno - 1].TData.Add(new FInfor(name, kind, count, price, null)); // null = 결제 수단을 넣는 곳인데, 주문에서는 결제가 불가능하기 때문에 null로 넣음
            }
        }

        public void initallfood(FDModel fdm, string data) //전체 리스트 추가
        {
            if (data == "김밥류" || data == "전체")
            {
                fdm.FData.Add(new AInfor("일반김밥", 2500, "김밥류", getimgPath("일반김밥")));
                fdm.FData.Add(new AInfor("참치김밥", 2500, "김밥류", getimgPath("참치김밥")));
                fdm.FData.Add(new AInfor("치즈김밥", 2500, "김밥류", getimgPath("치즈김밥")));
                fdm.FData.Add(new AInfor("돈까스김밥", 2500, "김밥류", getimgPath("돈까스김밥")));
                fdm.FData.Add(new AInfor("샐러드김밥", 2500, "김밥류", getimgPath("샐러드김밥")));
                fdm.FData.Add(new AInfor("소고기김밥", 2500, "김밥류", getimgPath("소고기김밥")));
            }
            if (data == "식사류" || data == "전체")
            {
                fdm.FData.Add(new AInfor("비빔밥", 6000, "식사류", getimgPath("비빔밥")));
                fdm.FData.Add(new AInfor("소고기덮밥", 7000, "식사류", getimgPath("소고기덮밥")));
                fdm.FData.Add(new AInfor("오므라이스", 6500, "식사류", getimgPath("오므라이스")));
                fdm.FData.Add(new AInfor("제육덮밥", 6500, "식사류", getimgPath("제육덮밥")));
            }
            if (data == "분식류" || data == "전체")
            {
                fdm.FData.Add(new AInfor("국물떡볶이", 6000, "분식류", getimgPath("국물떡볶이")));
                fdm.FData.Add(new AInfor("궁중떡볶이", 7000, "분식류", getimgPath("궁중떡볶이")));
                fdm.FData.Add(new AInfor("치즈떡볶이", 7000, "분식류", getimgPath("치즈떡볶이")));
                fdm.FData.Add(new AInfor("엽기떡볶이", 8000, "분식류", getimgPath("엽기떡볶이")));
            }
            if (data == "음료수" || data == "전체")
            {
                fdm.FData.Add(new AInfor("코카콜라", 2000, "음료수", getimgPath("코카콜라")));
                fdm.FData.Add(new AInfor("펩시", 2000, "음료수", getimgPath("펩시")));
            }
        }

        public string getimgPath(string foodname)
        {
            string picturepath = Directory.GetParent(System.Environment.CurrentDirectory).Parent.FullName.ToString() + "\\Image\\FoodImage\\" + foodname + ".jpg"; //현재 솔루션 경로 + 이미지 경로
            return picturepath;
        }

        public void InsertBuydata(string buytype, FModel model)
        {
            for (int i = 0; i < model.TData.Count; i++)
            {
                if (buytype.Equals("카드"))
                    bm.BuyList.Add(new FInfor(model.TData[i].FName, model.TData[i].FKind, model.TData[i].FCount, model.TData[i].FPrice, "카드")); //구매한 음식 데이터 삽입
                else
                    bm.BuyList.Add(new FInfor(model.TData[i].FName, model.TData[i].FKind, model.TData[i].FCount, model.TData[i].FPrice, "현금")); //구매한 음식 데이터 삽입
            }
        }
    }
}
