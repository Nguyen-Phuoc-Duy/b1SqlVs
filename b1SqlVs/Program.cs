using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace b1SqlVs

{
    internal class Program
    {

        private static void themCD(String constr)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                int PK_iCongdanID;
                string sHoten, sCMND, sHokhau;
                int soluongCD;
                DateTime tNgaysinh;
                Console.Write("Nhập số lượng cd muốn thêm: ");
                soluongCD = int.Parse(Console.ReadLine());
                int i = 0;
                while (i < soluongCD)
                {
                    Console.WriteLine("-----------------------------");
                    Console.WriteLine("Nhập CD thứ {0}", i + 1);
                    Console.Write("Nhập id CD: ");
                    PK_iCongdanID = int.Parse(Console.ReadLine());
                    Console.Write("Nhập họ tên CD: ");
                    sHoten = Console.ReadLine();
                    Console.Write("Nhập ngày sinh CD: ");
                    tNgaysinh = Convert.ToDateTime(Console.ReadLine());
                    Console.Write("Nhập CMND CD: ");
                    sCMND = Console.ReadLine();
                    Console.Write("Nhập ho khau CD: ");
                    sHokhau = Console.ReadLine();
                   

                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "proc_themCD";
                        cmd.Parameters.AddWithValue("@PK_iCongdanID", PK_iCongdanID);
                        cmd.Parameters.AddWithValue("@sHoten", sHoten);
                        cmd.Parameters.AddWithValue("@tNgaysinh", tNgaysinh);
                        cmd.Parameters.AddWithValue("@sCMND", sCMND);
                        cmd.Parameters.AddWithValue("@sHokhau", sHokhau);
                        cnn.Open();
                        int a = cmd.ExecuteNonQuery();
                        cnn.Close();
                        if (a > 0)
                        {
                            Console.WriteLine("Thêm thành công.");
                        }
                        else
                        {
                            Console.WriteLine("Thêm thất bại!");
                        }
                    }
                    i++;
                }

            }
        }


        private static void hienCD(String constr)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT* FROM tbl_Congdan", cnn))
                {
                    cnn.Open();
                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            Console.WriteLine("{0} {1} {2} {3} {4}",
                                read["PK_iCongdanID"], read["sHoten"], read["tNgaysinh"], read["sCMND"],
                                read["sHokhau"]);
                        }
                        read.Close();
                    }
                    cnn.Close();
                }
            }
        }

        private static void hienNamCD(string constr)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                int tNgaysinh;
                Console.Write("Nhập nam sinh: ");
                tNgaysinh = int.Parse(Console.ReadLine());

                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "proc_namsinh_hien";
                    cmd.Parameters.AddWithValue("@tNgaysinh", tNgaysinh);
                    cnn.Open();
                   
                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            Console.WriteLine("{0} {1} {2}",
                                read["sHoten"], read["sCMND"],
                                read["sHokhau"]);
                        }
                        read.Close();
                    }
                    cnn.Close();
                }
            }
        }


      
        private static void xoaCD(string constr)
                {
                    using (SqlConnection cnn = new SqlConnection(constr))
                    {
                        string sCMND;
                        Console.Write("Nhập CMND cần xóa: ");
                        sCMND = Console.ReadLine();
                        using (SqlCommand cmd = cnn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "proc_xoaCD";
                            cmd.Parameters.AddWithValue("@sCMND", sCMND);
                            cnn.Open();
                            int i = cmd.ExecuteNonQuery();
                            cnn.Close();
                            if (i > 0)
                            {
                                Console.WriteLine("Xóa thành công");
                            }
                            else
                            {
                                Console.WriteLine("Xóa thất bại");
                            }
                        }
                    }
                }


        static void Main(string[] args)
        {
            String constr = @"Data Source=ADMIN\SQLEXPRESS;Initial Catalog=ChungMinhNhanDan; Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            Console.OutputEncoding = Encoding.UTF8;
            while (true)
            {
                Console.WriteLine("----------MENU----------");
                Console.WriteLine("1.Thêm ds cong dan mới.");
                Console.WriteLine("2.Hiện DS cong dan.");
                Console.WriteLine("3.Hiện nam sinh cong dan.");
                Console.WriteLine("4.Xóa cong dan by CMND.");



                Console.Write("Nhập case: ");
                int luachon = Int32.Parse(Console.ReadLine());
                switch (luachon)
                {
                    case 1:
                        {
                            Program.themCD(constr);
                            break;
                        }
                    case 2:
                        {
                            Program.hienCD(constr);
                            break;
                        }
                    case 3:
                        {
                            Program.hienNamCD(constr);
                            break;
                        }

                    case 4:
                        {
                            Program.xoaCD(constr);
                            Program.hienCD(constr);
                            break;
                        }

                    default: break;
                }
            }
        }
    }
}
