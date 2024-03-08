﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laboratorio5
{
    public partial class Form1 : Form
    {
        List<Empleado> empleados = new List<Empleado>();
        List<Asistencia> asistencias = new List<Asistencia>();
        List<Reporte> reportes = new List<Reporte>();
        List<Buscador> buscadors = new List<Buscador>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CargarAsistencia();
            mostrar();
        }
        public void CargarAsistencia()
        {
            // leer archivo y cargar la lista
            string fileName = "Asistencia.txt";
            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);
            while (reader.Peek() > -1)
            {
                Asistencia asistencia = new Asistencia();
                asistencia.NoEmpleado = Convert.ToInt16(reader.ReadLine());
                asistencia.HorasMes = Convert.ToInt16(reader.ReadLine());
                asistencia.Mes = Convert.ToInt16(reader.ReadLine());

                asistencias.Add(asistencia);
            }
            reader.Close();
        }
        public void CargarEmpleados()
        {
            // leer archivo y cargar la lista
            string fileName = "Empleados.txt";
            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);
            while (reader.Peek() > -1)
            {
                Empleado empleado = new Empleado();
                empleado.NoEmpleado = Convert.ToInt16(reader.ReadLine());
                empleado.Nombre = reader.ReadLine();
                empleado.Sueldo = Convert.ToDecimal(reader.ReadLine());

                empleados.Add(empleado);
            }
            reader.Close();
        }
        public void mostrar()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = empleados;
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = asistencias;
            dataGridView2.Refresh();
            dataGridView1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Empleado empleado in empleados)
            {
                int noEmpleado = empleado.NoEmpleado;
                foreach (Asistencia asistencia in asistencias)
                {
                    if (empleado.NoEmpleado == asistencia.NoEmpleado)
                    {
                        Reporte reporte = new Reporte();
                        reporte.NombreEmpleado = empleado.Nombre;
                        reporte.Mes = asistencia.Mes;
                        reporte.SueldoPersona = empleado.Sueldo * asistencia.HorasMes;

                        reportes.Add(reporte);
                    }
                }
            }
            dataGridView3.DataSource = null;
            dataGridView3.DataSource = reportes;
            dataGridView3.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarEmpleados();
            comboBox1.DisplayMember = "Nombre";
            comboBox1.ValueMember = "NoEmpleado";
            comboBox1.DataSource = empleados;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int selectedEmpleadoId = (int)comboBox1.SelectedValue;
            Empleado selectedEmpleado = empleados.FirstOrDefault(emp => emp.NoEmpleado == selectedEmpleadoId);

            if (selectedEmpleado != null)
            {
                buscadors.Clear();
                List<Reporte> reportesEmpleado = reportes.Where(rep => rep.NombreEmpleado == selectedEmpleado.Nombre).ToList();
                foreach (var reporte in reportesEmpleado)
                {
                    Buscador buscador = new Buscador();
                    buscador.NombreEmpleado = selectedEmpleado.Nombre;
                    buscador.Mes = reporte.Mes;
                    buscador.SueldoPersona = reporte.SueldoPersona;
                    buscadors.Add(buscador);
                }

                dataGridView4.DataSource = null;
                dataGridView4.DataSource = buscadors;
                dataGridView4.Refresh();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
