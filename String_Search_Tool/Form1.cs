using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Data.SqlClient;

namespace String_Search_Tool
{
    public partial class Form1 : Form
    {
        Boolean sourcebox = false;
        Boolean searchLocbox = false;
        Boolean writebox = false;
        Boolean extbox = false;
        String writePath;
        String writeText;
        String sourcePath;
        String searchLocPath;
        //Setting up for multiple filetypes feature
        String[] fTypes;
        FileInfo results;
        String[] result;
        StreamWriter writeList;
        StreamReader fileContent;
        public string[] searchStrings;
        public string[] printStrings;
        String currentString;
        int numsearchStrings = 0;
        public Form1()
        {
            InitializeComponent();
            ToolTip sourceP = new ToolTip();
            sourceP.ShowAlways = true;
            sourceP.SetToolTip(source, "Provide file with list of strings for which to search.");
            ToolTip searchD = new ToolTip();
            searchD.ShowAlways = true;
            searchD.SetToolTip(txtFilepath, "Enter the directory in which to search.");
            ToolTip writeTo = new ToolTip();
            writeTo.ShowAlways = true;
            writeTo.SetToolTip(output, "Choose where the results file should be placed. Defaults to same location as source file");
            ToolTip ext = new ToolTip();
            ext.ShowAlways = true;
            ext.SetToolTip(Filetype, "Enter the desired file type (e.g. '.pl' or '.txt'). To list multiple extensions, place a comma between each. Leave the field blank to search all files.");
            ToolTip noSub = new ToolTip();
            noSub.ShowAlways = true;
            noSub.SetToolTip(oneDirectory, "Check to NOT search any subdirectories of the selected directory");
            ToolTip printFoundLines = new ToolTip();
            printFoundLines.ShowAlways = true;
            printFoundLines.SetToolTip(printLines, "Check to print every line which contains the search string");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFilepath.Text.ToString() == "" || source.Text.ToString() == "")
                {
                    MessageBox.Show("Please select a directory.");
                    return;
                }
                loadSearchCriteria();
                if (!writebox || writePath == "")
                    defaultWrite();
                writePath = writeText + "\\searchResults.txt";
                DirectoryInfo di = new DirectoryInfo(searchLocPath);
                results = new FileInfo(writePath);
                writeList = new StreamWriter(writePath);
                result = new String[numsearchStrings];
                for (int n = 0; n < result.Length; n++)
                    result[n] = "";
                //MessageBox.Show("yesh");
                setExtension();
                //searchAll(di);
                //devNewSearch(di);
                if(searchSQL.Checked)
                    sqlSearch();
                else
                    devNewSearch(di);
                //if(printLines.Checked)
                //    foreach (String foundString in result)
                //        if (foundString != null)
                //            if (foundString != "")
                //                writeList.Write(foundString + "\r\n-----------\r\n");

                
                if(printLines.Checked)
                    for (int p = 0; p < result.Length; p++)
                        if (notFound.Checked == false)
                        {
                            if (result[p] != "")
                                writeList.Write("-----------" + searchStrings[p] + "-----------\r\n" + result[p] + "\r\n");
                            else
                                writeList.Write("-----------" + searchStrings[p] + "-----------\r\nNOT FOUND.\r\n\r\n");
                        }
                        else
                        {
                            if (result[p] == "")
                                writeList.WriteLine(printStrings[p]);
                        }



                        
                 
                writeList.Close();
                MessageBox.Show("Search complete.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString());
            }
        }

        private void searchAll(DirectoryInfo info)
        {
            try
            {
                DirectoryInfo di = info;
                
                if (!oneDirectory.Checked)
                {
                    DirectoryInfo[] allSub = di.GetDirectories();
                    foreach (DirectoryInfo subDir in allSub)
                        searchAll(subDir);
                }

                FileInfo[] allFiles;
                foreach (String extension in fTypes)
                {
                    allFiles = di.GetFiles(extension);
                    fileContent = null;
                    string checkLine;
                    if (printLines.Checked)
                    {
                        bool isFound = false;
                        foreach (FileInfo fi in allFiles)
                        {
                            for (int j = 0; j < numsearchStrings; j++)
                                {
                                    fileContent = new StreamReader(fi.FullName);
                                    checkLine = fileContent.ReadLine();
                                    while (checkLine != null)
                                    {
                                        if (checkLine.ToUpper().Contains(searchStrings[j]))
                                        {
                                            if (!isFound)
                                                writeList.WriteLine(searchStrings[j] + " : " + fi.Name);
                                            writeList.WriteLine(checkLine);
                                            isFound = true;
                                        }
                                        checkLine = fileContent.ReadLine();
                                    }
                                    if (isFound)
                                        writeList.WriteLine();
                                    fileContent.Close();
                                    isFound = false;
                                }
                        }
                    }
                    else
                    {
                        bool[] found = new bool[numsearchStrings];
                        foreach (FileInfo fi in allFiles)
                        {
                            found = new bool[numsearchStrings];
                            fileContent = new StreamReader(fi.FullName);
                            checkLine = fileContent.ReadLine();
                            while (checkLine != null)
                            {
                                for (int j = 0; j < numsearchStrings; j++)
                                    if (!found[j])
                                        if (checkLine.ToUpper().Contains(searchStrings[j]))
                                        {
                                            writeList.WriteLine(printStrings[j] + " : " + fi.Name);
                                            writeList.WriteLine();
                                            found[j] = true;
                                        }
                                checkLine = fileContent.ReadLine();
                            }
                            fileContent.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString());
            }
        }

        private void loadSearchCriteria()
        {
            searchStrings = new String[5000];
            printStrings = new String[5000];
            try
            {
                numsearchStrings = 0;
                StreamReader searchSource = new StreamReader(sourcePath);
                currentString = searchSource.ReadLine();
                while (currentString != null)
                {
                    printStrings[numsearchStrings] = currentString;
                    searchStrings[numsearchStrings] = currentString.ToUpper();
                    numsearchStrings++;
                    currentString = searchSource.ReadLine();
                }
                searchSource.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void source_TextChanged(object sender, EventArgs e)
        {
            sourcePath = source.Text.ToString();
            sourcebox = true;
        }

        private void source_Click(object sender, System.EventArgs e)
        {
            source.ForeColor = Color.Black;
            if (!sourcebox)
                source.Clear();
            sourcebox = true;
        }

        private void output_TextChanged(object sender, EventArgs e)
        {
            writeText = output.Text.ToString();
            writebox = true;
        }

        private void output_Click(object sender, System.EventArgs e)
        {
            output.ForeColor = Color.Black;
            if (!writebox)
                output.Clear();
            writebox = true;
        }

        private void txtFilepath_TextChanged(object sender, EventArgs e)
        {
            searchLocPath = txtFilepath.Text.ToString();
            searchLocbox = true;
        }

        private void txtFilepath_Click(object sender, EventArgs e)
        {
            if (!searchLocbox)
                txtFilepath.Clear();
            txtFilepath.ForeColor = Color.Black;
            searchLocbox = true;
        }

        private void Filetype_Click(object sender, System.EventArgs e)
        {
            if (!extbox)
                Filetype.Clear();
            Filetype.ForeColor = Color.Black;
            extbox = true;
        }

        private void Filetype_TextChanged(object sender, EventArgs e)
        {
            if (Filetype.Text == "")
                excludeTypes.Enabled = false;
            else
                excludeTypes.Enabled = true;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = folderBrowserDialog1.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    txtFilepath.ForeColor = Color.Black;
                    txtFilepath.Text = folderBrowserDialog1.SelectedPath;
                    searchLocbox = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString());
            }
        }

        private void btnBrowse2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = openFileDialog1.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    source.ForeColor = Color.Black;
                    source.Text = openFileDialog1.FileName;
                    sourcebox = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString());
            }
        }

        private void btnBrowse3_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = folderBrowserDialog1.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    output.ForeColor = Color.Black;
                    output.Text = folderBrowserDialog1.SelectedPath;
                    writebox = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString());
            }
        }

        private void defaultWrite()
        {
            String[] path = sourcePath.Split(new char[] { '\\' });
            writePath = "";
            for (int j = 0; j < path.Length - 1; j++)
            {
                if (j == path.Length - 2)
                    writePath += path[j];
                else
                    writePath += path[j] + "\\";
            }
        }      
         
        private void setExtension()
        {
            if (!extbox || Filetype.Text == "")
                fTypes = new String[]{"*"};
            else
            {
                fTypes = Filetype.Text.Split(new char[] { ',' });
                for (int trim = 0; trim < fTypes.Length; trim++)
                    fTypes[trim] = fTypes[trim].Trim();
                for (int j = 0; j < fTypes.Length; j++)
                    if (fTypes[j].ToCharArray(0, 1)[0] == '.')
                        fTypes[j] = "*" + fTypes[j];
                    else
                        fTypes[j] = "*." + fTypes[j];
            }
        }

        private void devNewSearch(DirectoryInfo info)
        {
            try
            {
                DirectoryInfo di = info;

                if (!oneDirectory.Checked)
                {
                    DirectoryInfo[] allSub = di.GetDirectories();
                    foreach (DirectoryInfo subDir in allSub)
                        devNewSearch(subDir);
                }

                FileInfo[] allFiles;
                string checkLine;
                bool[] found = new bool[numsearchStrings];
                foreach (String extension in fTypes)
                {
                    allFiles = di.GetFiles(extension);
                    fileContent = null;
                    if (printLines.Checked)
                    {
                        foreach (FileInfo fi in allFiles)
                        {
                            found = new bool[numsearchStrings];
                            fileContent = new StreamReader(fi.FullName);
                            checkLine = fileContent.ReadLine();
                            while (checkLine != null)
                            {
                                for (int j = 0; j < numsearchStrings; j++)
                                {
                                    if (checkLine.ToUpper().Contains(searchStrings[j]))
                                    {
                                        if (!found[j])
                                            if(fullPath.Checked)
                                                result[j] += printStrings[j] + " : " + fi.FullName + "\r\n";
                                            else
                                                result[j] += printStrings[j] + " : " + fi.Name + "\r\n";
                                        result[j] += checkLine + "\r\n";
                                        found[j] = true;
                                    }
                                }
                                checkLine = fileContent.ReadLine();
                            }
                            fileContent.Close();
                            for (int spaces = 0; spaces < numsearchStrings; spaces++)
                                if (found[spaces])
                                    result[spaces] += "\r\n";
                        }
                      }
                    else
                    {
                        foreach (FileInfo fi in allFiles)
                        {
                            found = new bool[numsearchStrings];
                            fileContent = new StreamReader(fi.FullName);
                            checkLine = fileContent.ReadLine();
                            while (checkLine != null)
                            {
                                for (int j = 0; j < numsearchStrings; j++)
                                    if (!found[j])
                                        if (checkLine.ToUpper().Contains(searchStrings[j]))
                                        {
                                            if (fullPath.Checked)
                                                writeList.WriteLine(printStrings[j] + " : " + fi.FullName);
                                            else
                                                writeList.WriteLine(printStrings[j] + " : " + fi.Name);
                                            writeList.WriteLine();
                                            found[j] = true;
                                        }
                                checkLine = fileContent.ReadLine();
                            }
                            fileContent.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString());
            }
        }

        private void placeHolder_CheckedChanged(object sender, EventArgs e)
        {
            if (placeHolder.Checked == true)
                btnBrowse2.Enabled = false;
            else
                btnBrowse2.Enabled = true;
        }
        /*
         String[] results = new String[numsearchStrings];
					bool[] found = new bool[numsearchStrings];
                        foreach (FileInfo fi in allFiles)
                        {
                            found = new bool[numsearchStrings];
                            fileContent = new StreamReader(fi.FullName);
                            checkLine = fileContent.ReadLine();
                            while (checkLine != null)
                            {
                                for (int j = 0; j < numsearchStrings; j++)
									{
										if (checkLine.ToUpper().Contains(searchStrings[j]))
											{
												if (!found[j])
													results[j] += printStrings[j] + " : " + fi.Name + "\r\n";
												results[j] += checkLine + "\r\n";
												found[j] = true;
											}
									}
                                checkLine = fileContent.ReadLine();
                            }
							fileContent.Close();
                        } 
         */




        //Working Search Method
        /*
         * 
         * private void searchAll(DirectoryInfo info)
        {
            try
            {
                DirectoryInfo di = info;
                
                if (!oneDirectory.Checked)
                {
                    DirectoryInfo[] allSub = di.GetDirectories();
                    foreach (DirectoryInfo subDir in allSub)
                        searchAll(subDir);
                }

                FileInfo[] allFiles;
                foreach (String extension in fTypes)
                {
                    allFiles = di.GetFiles(extension);
                    fileContent = null;
                    string checkLine;
                    if (printLines.Checked)
                    {
                        bool isFound = false;
                        foreach (FileInfo fi in allFiles)
                        {
                            for (int j = 0; j < numsearchStrings; j++)
                                {
                                    fileContent = new StreamReader(fi.FullName);
                                    checkLine = fileContent.ReadLine();
                                    while (checkLine != null)
                                    {
                                        if (checkLine.ToUpper().Contains(searchStrings[j]))
                                        {
                                            if (!isFound)
                                                writeList.WriteLine(searchStrings[j] + " : " + fi.Name);
                                            writeList.WriteLine(checkLine);
                                            isFound = true;
                                        }
                                        checkLine = fileContent.ReadLine();
                                    }
                                    if (isFound)
                                        writeList.WriteLine();
                                    fileContent.Close();
                                    isFound = false;
                                }
                        }
                    }
                    else
                    {
                        bool[] found = new bool[numsearchStrings];
                        foreach (FileInfo fi in allFiles)
                        {
                            found = new bool[numsearchStrings];
                            fileContent = new StreamReader(fi.FullName);
                            checkLine = fileContent.ReadLine();
                            while (checkLine != null)
                            {
                                for (int j = 0; j < numsearchStrings; j++)
                                    if (!found[j])
                                        if (checkLine.ToUpper().Contains(searchStrings[j]))
                                        {
                                            writeList.WriteLine(printStrings[j] + " : " + fi.Name);
                                            writeList.WriteLine();
                                            found[j] = true;
                                        }
                                checkLine = fileContent.ReadLine();
                            }
                            fileContent.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString());
            }
        }
         * 
         */

        public void sqlSearch()
        {
            //SqlConnection conn = new SqlConnection("server=VMTESTRAPTOR.7-11T.com;user=test;password=73l3v3n;database=InventoryControlSystem");
            SqlConnection conn = new SqlConnection("server=raptor;user=sa;password=fustigate;database=InventoryControlSystem");
            SqlDataReader Reader;
            String content = @"SELECT distinct o.[name], c.text
FROM InventoryControlSystem.dbo.sysobjects o (NOLOCK) JOIN InventoryControlSystem.dbo.syscomments c (NOLOCK) ON o.id = c.id 
where c.text like @SP_NAME
UNION ALL
SELECT distinct o.[name], c.text
FROM Operations.dbo.sysobjects o (NOLOCK) JOIN Operations.dbo.syscomments c (NOLOCK) ON o.id = c.id 
where c.text like @SP_NAME
UNION ALL
SELECT distinct o.[name], c.text
FROM Operations_Development.dbo.sysobjects o (NOLOCK) JOIN Operations_Development.dbo.syscomments c (NOLOCK) ON o.id = c.id 
where c.text like @SP_NAME
UNION ALL
SELECT distinct o.[name], c.text
FROM OPIS_DEV.dbo.sysobjects o (NOLOCK) JOIN OPIS_DEV.dbo.syscomments c (NOLOCK) ON o.id = c.id 
where c.text like @SP_NAME
UNION ALL
SELECT distinct o.[name], c.text
FROM test.dbo.sysobjects o (NOLOCK) JOIN test.dbo.syscomments c (NOLOCK) ON o.id = c.id 
where c.text like @SP_NAME";
//UNION ALL
//SELECT      j.job_id,   j.originating_server,
//      j.name,     js.step_id, js.command, j.enabled 
//FROM  MSDB.dbo.sysjobs j JOIN MSDB.dbo.sysjobsteps js ON    js.job_id = j.job_id 
//WHERE js.command LIKE @SP_NAME";


//            content = @"SELECT j.name, js.command, j.enabled 
//FROM  MSDB.dbo.sysjobs j JOIN MSDB.dbo.sysjobsteps js ON    js.job_id = j.job_id 
//WHERE j.name LIKE @SP_NAME and j.enabled = 1";

            content = @"SELECT distinct o.[name], 'InventoryControlSystem'
FROM InventoryControlSystem.dbo.sysobjects o (NOLOCK) JOIN InventoryControlSystem.dbo.syscomments c (NOLOCK) ON o.id = c.id 
where c.text like @SP_NAME
UNION ALL
SELECT distinct o.[name], 'Operations'
FROM Operations.dbo.sysobjects o (NOLOCK) JOIN Operations.dbo.syscomments c (NOLOCK) ON o.id = c.id 
where c.text like @SP_NAME
UNION ALL
SELECT distinct o.[name], 'Operations_Development'
FROM Operations_Development.dbo.sysobjects o (NOLOCK) JOIN Operations_Development.dbo.syscomments c (NOLOCK) ON o.id = c.id 
where c.text like @SP_NAME
UNION ALL 
SELECT distinct o.[name], 'OPIS_DEV'
FROM OPIS_DEV.dbo.sysobjects o (NOLOCK) JOIN OPIS_DEV.dbo.syscomments c (NOLOCK) ON o.id = c.id 
where c.text like @SP_NAME";
//UNION ALL
//SELECT distinct o.[name]
//FROM test.dbo.sysobjects o (NOLOCK) JOIN test.dbo.syscomments c (NOLOCK) ON o.id = c.id 
//where c.text like @SP_NAME";
            try
            {
                conn.Open();
                SqlCommand ds = new SqlCommand(content, conn);
                ds.CommandType = CommandType.Text;
                //CommandType.StoredProcedure;
                //DataSet df = new DataSet();
                SqlParameter SPName = new SqlParameter("@SP_NAME", SqlDbType.VarChar);
                //SPName.Value = "";
                ds.Parameters.Add(SPName);
                bool found = false;
                foreach (String sps in printStrings)
                {
                    if (sps != null)
                        if (sps != "")
                        {
                            found = false;
                            ds.Parameters["@SP_NAME"].Value = "%" + sps + "%";
                            //ds.Parameters.Clear();
                            //SPName.Value = sps;
                            //ds.Parameters.AddWithValue("@SP_NAME", sps);
                            //ds.Parameters.Add(SPName);
                            Reader = ds.ExecuteReader();
                            //MessageBox.Show(Reader[0].ToString());
                            while (Reader.Read())
                            {
                                //if (!found)
                                //{
                                //    writeList.WriteLine(sps);
                                //    found = true;
                                //}
                                //for (int j = 0; j < Reader.FieldCount; j++)

                                
                                if (!found)
                                    writeList.WriteLine("----------------------" + sps+ "-----------------------");
                                writeList.WriteLine(String.Format("{0}\tDB: {1}", Reader[0], Reader[1]));
                                found = true;
                                 

                                //MessageBox.Show(sps + " : " + String.Format("{0}", Reader[j]));
                                //writeList.WriteLine(String.Format("{0}", Reader[0]));
                                //writeList.WriteLine();
                                
                            }
                            Reader.Close();
                        }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
