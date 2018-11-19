using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DGSW_POS.Model;
using DGSW_POS.Controller;

namespace DGSW_POS.View
{
    public partial class Chart : Form
    {
        public Chart()
        {
            InitializeComponent();
            this.CenterToScreen();
            Initcategorychart(); //카테고리 리스트뷰 데이터 추가 메소드
            Initmenuchart(); //메뉴별 리스트뷰 데이터 추가 메소드
            initmoneycard(); //카드, 현금 별 가격 표시 메소드
        }

        private void Initcategorychart()
        {
            string[] category = { "김밥류", "식사류", "분식류", "음료수" };
            int [] categoryct = new int[4];
            int [] categoryprice = new int[4];
            Main.bf.setcategoryData(categoryct, categoryprice); //카테고리별 통계 데이터 생성 요청
            for(int i = 0; i < category.Length; i++)
            {
                ListViewItem lvt = new ListViewItem();
                lvt.Text = i + 1 + "";
                lvt.SubItems.Add(category[i]);
                lvt.SubItems.Add(categoryct[i] + "");
                lvt.SubItems.Add(categoryprice[i] + "");
                this.listView1.Items.Add(lvt);
            }
        }
        private void Initmenuchart()
        {
            List<CMenuModel> kindList = new List<CMenuModel>(); //메뉴별 통계 리스트 (CMenuModel 이용)
            List<string> foodname = new List<string>(); //한 개의 음식이름 찾기 위한 리스트 

            Main.bf.addmenuchartData(kindList, foodname); //초기 데이터 컨트롤러에서 추가

            foodname = foodname.Distinct().ToList(); //중복 음식이름 제거
            string[] foodlist = foodname.ToArray(); //배열로 저장

            int[] foodcount = new int[foodlist.Length]; //음식 개수만큼 배열 생성
            int[] foodprice = new int[foodlist.Length];

            for(int i = 0; i < foodlist.Length; i++) //음식 개수만큼 반복
            {
                for(int j = 0; j < kindList.Count; j++) //리스트 개수만큼 반복
                {
                    if (kindList[j].FName.Equals(foodlist[i])) //리스트의 이름과 음식의 이름과 같으면
                    {
                        //값 추가
                        foodprice[i] += kindList[j].FPrice;
                        foodcount[i] += kindList[j].FCount;
                    }
                }
            }
            //데이터 추가
            for(int i = 0; i < foodlist.Length; i++)
            {
                ListViewItem lvt = new ListViewItem();
                lvt.Text = i + 1 + "";
                lvt.SubItems.Add(foodname[i]);
                lvt.SubItems.Add(foodcount[i] + "");
                lvt.SubItems.Add(foodprice[i] + "");
                this.listView2.Items.Add(lvt);
            }
        }
        private void initmoneycard()
        {
            int all = 0;
            int money = 0;
            int card = 0;
            this.label4.Text = Main.bf.setmoneycardData(all, money, card);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Chart_FormClosing(object sender, FormClosingEventArgs e)
        {
            Main.bf.backmain();
        }
    }
}
