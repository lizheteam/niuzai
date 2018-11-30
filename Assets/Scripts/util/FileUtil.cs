
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class FileUtil
{
    /// <summary>
    /// 获取当前运行路径
    /// </summary>
    /// <returns></returns>
    public static string GetRunDirectory()
    {
        return Environment.CurrentDirectory;
    }

    /// <summary>
    /// 获取当前运行路径的父路径
    /// </summary>
    /// <returns></returns>
    public static string GetRunDirectoryInParentPath(int PathNum)
    {
        //获取当前运行路径
        //对于我们当前程序的运行程序来说，当前存储该程序的目录即为他的父对象
        DirectoryInfo pathInfo = Directory.GetParent(GetRunDirectory());
        while (PathNum > 0 || pathInfo.Parent != null)
            pathInfo = pathInfo.Parent;
        //获取到一个完整的文件夹路径
        return pathInfo.FullName;
    }

    /// <summary>
    /// 创建一个文件夹
    /// </summary>
    /// <param name="path">文件夹路径</param>
    /// <param name="foldName">文件夹名字</param>
    /// <returns></returns>
    public static string CreateFolder(string path)
    {
        //如果路径不存在，则创建一个目录文件夹
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        return path;
    }

    /// <summary>
    /// 创建一个文本对象
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <param name="fileName">文件名称</param>
    /// <param name="info">文件信息</param>
    public static void CreateFile(string path,string fileName,string info)
    {
        //数据流对象
        StreamWriter streamWrite;
        //拼接一个文本文件对象
        FileInfo finfo = new FileInfo(path + "//" + fileName);
        //判断文件夹是否存在，如果不存在，则创建一个目录
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        //如果该文件已经存在，则直接删除
        if(finfo .Exists)
        {
            finfo.Delete();
        }
        //创建一个文本对象，并返回给流对象
        streamWrite = finfo.CreateText();
        //写入数据
        streamWrite.WriteLine(info);
        //写入完成后关闭
        streamWrite.Close();
        //销毁
        streamWrite.Dispose();
    }

    /// <summary>
    /// 向一个文件内添加一些数据
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <param name="fileName">文件名称</param>
    /// <param name="info">文件信息</param>
    public static void AddFile(string path,string fileName,string info)
    {
        //数据流对象
        StreamWriter streamWrite;
        //拼接一个文本文件对象
        FileInfo finfo = new FileInfo(path + "//" + fileName);
        //判断文件夹是否存在，如果不存在，则创建一个目录
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        //如果文件不存在，则创建一个文件，否则加载该文件，并将文件对象赋值给流对象
        streamWrite = !File.Exists(path) ? File.CreateText(path) : File.AppendText(path);
        streamWrite.WriteLine(info);
        streamWrite.Close();
        streamWrite.Dispose();
    }

    /// <summary>
    /// 读取文件
    /// </summary>
    /// <param name="path"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static string LoadFile(string path,string fileName)
    {
        if (!IsExistsFile(path, fileName)) return null;
        //创建一个读入流对象，打开一个文本文件
        StreamReader streamRead = File.OpenText(path + "//" + fileName);
        ArrayList arr = new ArrayList();
        while (true)
        {
            //按行读取文本内容部
            string line = streamRead.ReadLine();
            //如果读取内容到最后一行的下一行，跳出循环
            if(line ==null)
            {
                break;
            }
            arr.Add(line);
        }
        //将读取到的内容添加到str字符串中
        string str = "";
        foreach (string i in arr)
            str += i;
        //关闭数据流对象
        streamRead.Close();
        //销毁流对象
        streamRead.Dispose();
        //返回读取的内容
        return str;
    }

    /// <summary>
    /// 文件是否存在
    /// </summary>
    /// <param name="path"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static bool IsExistsFile(string path,string fileName)
    {
        //如果文件不存在的话，直接返回不存在
        if (!Directory.Exists(path))
            return false;
        //如果文件不存在的话，返回不存在
        if (!File.Exists(path + "//" + fileName))
            return false;
        return true;
    }
}

