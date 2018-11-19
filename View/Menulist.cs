using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using DGSW_POS.Controller;
using DGSW_POS.Model;
using System.Diagnostics;

namespace DGSW_POS.View
{
    public partial class Menulist : Form
    {
        private int tablect; //테이블 번호 저장 변수
        FDModel fdm = new FDModel();
        FoodButton[] fb; //버튼 배열 생성
        string buytype = "";

        private string BarcodeStr = "";
        private Boolean isBarcode = false;

        public Menulist(int tct)
        {
            InitializeComponent();
            this.button1.Select();
            this.textBox1.Select();
            this.textBox1.Visible = false;
            tablect = tct; //선택한 테이블 번호 변수에 저장
            this.Tablelabel.Text = tablect + "번 테이블";
            if (AForm.fm[tablect-1] == null) //이전 데이터가 없다면
            {
                AForm.fm[tablect-1] = new FModel(); //데이터 저장할 객체 생성
                AForm.fm[tablect-1].TNo = tablect; //테이블 번호 모델에 저장
                this.panel1.Visible = false; //결제 패널 안보에기 설정
            }
            else //아니면
            {
                loaddata(); //데이터 불러오기
                this.panel1.Visible = true; //결제 패널 보이게 설정
                
            }
            setordertime(); //주문 시간 설정
            clearfoodlist("전체");
        }

        private void loaddata()
        {
            FInfor[] info = AForm.fm[tablect - 1].TData.ToArray();
            for(int i = 0; i < info.Length; i++)
            {
                ListViewItem lvt = new ListViewItem();
                lvt.Text = info[i].FName;
                lvt.SubItems.Add(info[i].FCount + "");
                lvt.SubItems.Add(info[i].FPrice + "");
                lvt.SubItems.Add(info[i].FKind);
                this.listView1.Items.Add(lvt);
            }
            setfoodprice();
        }

        private void kindbtn_Click(object sender, EventArgs e) //종류선택 버튼 클릭 이벤트
        {
            Button b = sender as Button;
            clearfoodlist(b.Text);
        }

        private void clearfoodlist(String data) //음식 리스트 초기화
        {
            this.flowLayoutPanel1.Controls.Clear(); //버튼들 제거
            fdm.FData.Clear();
            Main.af.initallfood(fdm, data);
            fb = new FoodButton[fdm.FData.Count];
            for(int i = 0; i < fb.Length; i++)
            {
                fb[i] = new FoodButton();
                flowLayoutPanel1.Controls.Add(fb[i]);
                fb[i].picturebtn.Name = i+""; //버튼 이름을 인덱스로 저장
                fb[i].setpicture = fdm.FData[i].FPath;
                fb[i].setnamelabel = fdm.FData[i].FName;
                fb[i].setpricelabel = fdm.FData[i].FPrice.ToString();
                fb[i].picturebtn.Click += new EventHandler(Foodbtn_click);
            }

        }

        protected void Foodbtn_click(object sender, EventArgs e)
        {
            Button btn = sender as Button; //어느 버튼을 선택했는지 가져옴.
            int bct = int.Parse(btn.Name); //클릭한 버튼 번호 저장
            for(int i = 0; i < this.listView1.Items.Count; i++) //이미 선택한 음식이 있는지 검사
            {
                if (this.listView1.Items[i].Text.Equals(fb[bct].namelabel.Text)) //중복된 것이 있다면
                {
                    MessageBox.Show("이미 선택된 음식입니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            ListViewItem lvt = new ListViewItem();
            lvt.Text=fb[bct].namelabel.Text; //음식 이름
            lvt.SubItems.Add(1+""); //수량
            lvt.SubItems.Add(fb[bct].pricelabel.Text);//가격
            lvt.SubItems.Add(fdm.FData[bct].FKind); //종류
            this.listView1.Items.Add(lvt); //ListView에 데이터 추가
            this.pictureBox1.Image = System.Drawing.Image.FromFile(fdm.FData[bct].FPath); //미리보기 이미지 설정
            setfoodprice(); //총 가격 설정
        }

        private void setfoodprice()
        {
            int price = 0;

            for(int i = 0; i < this.listView1.Items.Count; i++)
            {
                price += int.Parse(this.listView1.Items[i].SubItems[2].Text);
            }
            this.pricelabel.Text = price + "원";
        }

        private void Menulist_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }


        private void setordertime()
        {
             this.ordertimelabel.Text = DateTime.Now.ToString(string.Format("yyy년 MM월 dd일 HH시 mm분"));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.listView1.Items.Clear();
            setfoodprice();
        }

        private void listView1_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e) //listview 컬럼 크기 제어
        {
            e.NewWidth = this.listView1.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < this.listView1.Items.Count; i++)
            {
                if (this.listView1.Items[i].Selected == true)
                {
                    this.listView1.Items[i].Remove();
                    setfoodprice();
                }
            }
        }

        private void button8_Click(object sender, EventArgs e) //수량 증가 버튼
        {
            ChangeValue(1);
        }

        private void button9_Click(object sender, EventArgs e) //수량 감소 버튼
        {
            ChangeValue(-1);
        }

        private void ChangeValue(int changeValue)
        {
            for (int i = 0; i < this.listView1.Items.Count; i++)
            {
                if (this.listView1.Items[i].Selected == true) //선택했으면
                {
                    int foodct = int.Parse(this.listView1.Items[i].SubItems[1].Text.ToString()); //수량
                    int price = int.Parse(this.listView1.Items[i].SubItems[2].Text.ToString()); //가격
                    int firstprice = price / foodct; //개당 가격
                    if (foodct == 1 && changeValue==-1) //수량이 1이고 수량 감소 버튼을 눌렀으면
                    {
                        MessageBox.Show("최소 1개 이상의 수량을 선택해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    foodct = foodct + changeValue;
                    if (changeValue == 1) //증가
                    {
                        price += firstprice;
                    }
                    else if (changeValue == -1) //감소
                    {
                        price -= firstprice;
                    }
                    this.listView1.Items[i].SubItems[1].Text = foodct + "";
                    this.listView1.Items[i].SubItems[2].Text = price + "";
                    setfoodprice(); //총 가격 설정
                    return;
                }
            }
            MessageBox.Show("음식을 선택해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error); //선택하지 않았으면 에러 메시지
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Main.af.insertdata(this.listView1); //주문한 음식 데이터 저장
            this.Close(); //폼 닫기
        }

        private void Menulist_FormClosing(object sender, FormClosingEventArgs e) //폼이 닫힐 때
        {
            Main.af.backmenu(); //메인으로 이동
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i= 0; i< this.listView1.Items.Count; i++){
                for(int j=0; j < fdm.FData.Count; j++)
                {
                    if (this.listView1.Items[i].SubItems[0].Text.Equals(fdm.FData[i].FName))
                    {
                        this.pictureBox1.Image = System.Drawing.Image.FromFile(fdm.FData[i].FPath);
                        return;
                    }
                }
            }
        }

        private void radioButton_Click(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            buytype = rb.Text;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (this.listView1.Items.Count == 0) //선택한 음식이 없는데 주문하면
            {
                MessageBox.Show("먼저 음식을 선택해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error); //선택하지 않았으면 에러 메시지
                return;
            }
            if(buytype=="")//결제수단을 선택하지 않으면
            {
                MessageBox.Show("결제 수단을 선택해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error); //선택하지 않았으면 에러 메시지
                return;
            }
            Checkorder chkorder = new Checkorder(this, AForm.fm[tablect-1], buytype, tablect-1);
            chkorder.Show();
        }

        private void Menulist_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.textBox1.Select();
            if (!isBarcode)
            {
                isBarcode = true;
                BarcodeStr="";
            }
            char data = e.KeyChar;
            if (data.Equals('\r'))
            {
                isBarcode = false;
                ListViewItem lvt = new ListViewItem();
                if (BarcodeStr.Equals("8801094017200"))
                {
                    if (!checkisExistBarcode("코카콜라"))
                        return;
                    lvt.Text = "코카콜라";
                    lvt.SubItems.Add(1 + ""); //수량
                    lvt.SubItems.Add(2000 + "");//가격
                    lvt.SubItems.Add("음료수"); //종류
                    this.listView1.Items.Add(lvt);
                    this.pictureBox1.Image = System.Drawing.Image.FromFile(Main.af.getimgPath("코카콜라")); //미리보기 이미지 설정
                }
                else if (BarcodeStr.Equals("8801056070809"))
                {
                    if (!checkisExistBarcode("펩시"))
                        return;
                    lvt.Text = "펩시";
                    lvt.SubItems.Add(1 + ""); //수량
                    lvt.SubItems.Add(2000 + "");//가격
                    lvt.SubItems.Add("음료수"); //종류
                    this.listView1.Items.Add(lvt);
                    this.pictureBox1.Image = System.Drawing.Image.FromFile(Main.af.getimgPath("펩시")); //미리보기 이미지 설정
                }
                else
                {
                    MessageBox.Show("등록되지 않은 상품입니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error); //선택하지 않았으면 에러 메시지
                    return;
                }
                return;
            }
            Debug.WriteLine(data);
            BarcodeStr += data;
        }
        private bool checkisExistBarcode(string name)
        {
            for(int i = 0; i < this.listView1.Items.Count; i++)
            {
                if (this.listView1.Items[i].SubItems[0].Text.Equals(name))
                {
                    MessageBox.Show("이미 선택된 음식입니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }
    }
}
