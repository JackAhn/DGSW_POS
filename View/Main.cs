using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using DGSW_POS.Model;
using DGSW_POS.View;
using DGSW_POS.Controller;

namespace DGSW_POS
{
    public partial class Main : Form
    {
        public static AForm af=new AForm(); //메인과 주문 폼 컨트롤러 객체 생성, 주문 폼에서도 사용.
        public static BForm bf = new BForm(); //메인과 통계 폼 컨트롤러 객체 생성, 통계 폼에서도 사용.
        public Main()
        {
            InitializeComponent();
            af.initmain(this);
            bf.initmain(this);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            settimer();
        }

        private void settimer() //폼 활성화 시 처음 시간 설정
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture("ko-KR");
            this.timelabel.Text = DateTime.Now.ToString(string.Format("yyyy-MM-dd ddd요일 HH시 mm분 ss초", culture));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture("ko-KR");
            this.timelabel.Text = DateTime.Now.ToString(string.Format("yyyy-MM-dd ddd요일 HH시 mm분 ss초", culture));
        }

        private void Main_Activated(object sender, EventArgs e)
        {
            this.timer1.Start();
            updatefoodlist();
        }


        private void updatefoodlist()
        {
            Label[] lb = { this.label10, this.label11, this.label12, this.label13, this.label14, this.label15, this.label16, this.label17 }; //메뉴 리스트 표시할 라벨 배열 생성
            for(int i = 0; i< lb.Length; i++)
            {
                string foodtext = ""; //표시할 음식 이름 및 수량 텍스트 변수.
                if (AForm.fm[i] != null) //해당 테이블에 데이터가 있다면
                {
                    FInfor [] data = AForm.fm[i].TData.ToArray(); //선택한 테이블의 데이터 배열로 가져옴.
                    for(int j = 0; j < data.Length; j++)
                    {
                        foodtext += data[j].FName + " x" + data[j].FCount + "\n"; //텍스트 추가
                    }
                }
                lb[i].Text = foodtext; //라벨 텍스트 설정
            }
        }

        private void Main_Deactivate(object sender, EventArgs e)
        {
            this.timer1.Stop();
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button b = sender as Button; //누른 버튼 가져오기
            int buttonct = int.Parse(b.Name.Replace("button", "")); //몇 번째 버튼을 눌렀는지 숫자만 가져오기 위해 앞의 button 문자 공백으로 변경 후 숫자로
            af.showmenu(buttonct);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            bf.showchart();
        }

        private void Listlabel_Click(object sender, EventArgs e)
        {
            Label b = sender as Label; //누른 라벨 가져오기
            int labelct = int.Parse(b.Name.Replace("label", "")) - 9; //몇 번째 버튼을 눌렀는지 숫자만 가져오기 위해 앞의 label 문자 공백으로 변경 후 숫자로
            af.showmenu(labelct);
        }
    }
}
