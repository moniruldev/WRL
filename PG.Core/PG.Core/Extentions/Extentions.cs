using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace PG.Core.Extentions
{
    public static class Extentions
    {
        public static object ToType<T>(this object obj, T type)
        {
            //Example
            //object obj=getSomeObjectOfAnonymoustype();
            //Client client=obj.ToType(typeof (Client));


            //create instance of T type object:
            var tmp = Activator.CreateInstance(Type.GetType(type.ToString()));

            //loop through the properties of the object you want to covert:          
            foreach (PropertyInfo pi in obj.GetType().GetProperties())
            {
                try
                {

                    //get the value of property and try 
                    //to assign it to the property of T type object:
                    tmp.GetType().GetProperty(pi.Name).SetValue(tmp, pi.GetValue(obj, null), null);
                }
                catch { }
            }

            //return the T type object:         
            return tmp;
        }

        public static object ToNonAnonymousList<T>(this List<T> list, Type t)
        {
            //example
            //.ToList().ToNonAnonymousList(typeof(Client))


            ////apply extension  to covert into Client
            //.ToNonAnonymousList(typeof(Client))
            //;

            
            

            //define system Type representing List of objects of T type:
            var genericType = typeof(List<>).MakeGenericType(t);

            //create an object instance of defined type:
            var l = Activator.CreateInstance(genericType);

            //get method Add from from the list:
            MethodInfo addMethod = l.GetType().GetMethod("Add");

            //loop through the calling list:
            foreach (T item in list)
            {

                //convert each object of the list into T object 
                //by calling extension ToType<T>()
                //Add this object to newly created list:
                addMethod.Invoke(l, new object[] { item.ToType(t) });
            }

            //return List of T objects:
            return l;
        }
        public static T ConvertToType<T>(this object obj) where T : class , new()
        {
            //	If source is null, return
            if (obj == null)
                return null;

            //	Get the type of each object
            Type sourceType = obj.GetType();

            Type targetType = typeof(T);
            T destObject = new T();

            //	Loop through the source properties
            foreach (PropertyInfo p in sourceType.GetProperties())
            {
                //	Get the matching property in the destination object
                PropertyInfo targetObj = targetType.GetProperty(p.Name);
                //	If there is none, skip
                if (targetObj == null)
                    continue;
                if (p.GetType() != targetObj.GetType())
                    throw new InvalidCastException("Property type not matched");

                //	Set the value in the destination
                if (targetObj.CanWrite)
                {
                    if (p.GetType() != targetObj.GetType())
                        throw new InvalidCastException("Property type not matched");
                    targetObj.SetValue(destObject, p.GetValue(obj, null), null);
                }
            }
            return destObject;
        }

        //public static List<T> ConvertALLToType<T>(this List<T> list) where T : class, new()
        //{
        //    //example
        //    //.ToList().ToNonAnonymousList(typeof(Client))


        //    ////apply extension  to covert into Client
        //    //.ToNonAnonymousList(typeof(Client))
        //    //;



        //    //define system Type representing List of objects of T type:
        //   // var genericType = typeof(List<>).MakeGenericType(typeof(T));

        //    List<T> listNew = new List<T>();

        //    //create an object instance of defined type:
        //    //var l = Activator.CreateInstance(genericType);

        //    //get method Add from from the list:
        //    //MethodInfo addMethod = l.GetType().GetMethod("Add");

        //    //loop through the calling list:
        //    foreach (L item in list)
        //    {
        //        //Type t = typeof(T).GetType();
                
        //        listNew.Add(item.ConvertToType<T>());
                
        //        //convert each object of the list into T object 
        //        //by calling extension ToType<T>()
        //        //Add this object to newly created list:
                
        //        //addMethod.Invoke(l, new object[] { item.ToType(t) });
        //    }

        //    //return List of T objects:
        //    //return l;
        //    return listNew;
        //}

        public static T Cast<T>(this object obj, T type)
        {
            return ((T)obj);


            ///use example
            //            static void Main(string[] args)
            //{
            //TestAnonType (new { Test1 = 1, Test2 = "Testing" } as object);
            //TestAnonType2(new { Test1 = 2, Test2 = "Testing" } as object);
            //}

            //private static void TestAnonType<T>(T o ) where T : class
            //{
            //var t = o.Cast(new { Test1 = 0, Test2 = string.Empty });
            //Console.WriteLine(
            //string.Format(
            //"Test1: {0}\tTest2: {1}",
            //t.Test1,
            //t.Test2
            //)
            //); 

        } 


    }
}
