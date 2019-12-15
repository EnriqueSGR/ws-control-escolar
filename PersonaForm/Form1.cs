using PersonaForm.Models.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace PersonaForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = "https://localhost:44391/api/persona";

            PersonasRequest oPersona = new PersonasRequest();
            oPersona.Nombre = txtNombre.Text;
            oPersona.Edad = int.Parse(txtEdad.Text);

            

            //primer parametro url
            //segundo parametro el objeto
            //tercer parametro el metodo: POST, GET, PUT, DELETE
            string resultado = Send<PersonasRequest>(url, oPersona, "POST");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

       


        public string Send<T>(string url, T objectRequest, string method = "POST")
        {
            string result = "";
            

            try
            {
                

                JavaScriptSerializer js = new JavaScriptSerializer();

                //serializamos el objeto
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(objectRequest);

                //peticion
                WebRequest request = WebRequest.Create(url);
                //headers
                request.Method = method;
                request.PreAuthenticate = true;
                request.ContentType = "application/json;charset=utf-8'";
                request.Timeout = 10000; //esto es opcional

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }

                var httpResponse = (HttpWebResponse)request.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
              
            }
            catch (Exception e)
            {

                result = e.Message;

            }

            return result;
        }
    }
}

