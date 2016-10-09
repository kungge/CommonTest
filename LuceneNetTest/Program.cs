using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuceneNetTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();

            List<BlogInfo> list = new List<BlogInfo>();
            list.Add(new BlogInfo() { Id=1,BlogTitle= "手机丢失后支付宝微信里的处理方法", BlogContent="国庆小长假近在眼前，制定好的出游计划马上就能实现，想必大家的心情一定很激动吧。作为一个“没有手机会死星”人，手机对于出游来说太重要了，不然怎么在朋友圈晒九宫格自拍呢。想象一下十一期间各大景区的“people mountain people sea”，很容易发生“人间惨剧”：一个不小心手机被偷了。。。。",ViewCount=829 });
            list.Add(new BlogInfo() { Id = 2, BlogTitle = "三星Note7深陷爆炸门", BlogContent = "三星Galaxy Note 7手机在发布后一个多月的时间里，先后发生了多起因电池缺陷造成的燃爆事故。三星Note7“爆炸门”发生后，三星宣布召回全球250万部Note 7手机。。。。", ViewCount = 69 });
            list.Add(new BlogInfo() { Id = 3, BlogTitle = "阿里巴巴四员工编程抢月饼被开除", BlogContent = "中秋节，各个公司纷纷给员工发月饼送去节日的问候，本来是件挺开心的事情，可是阿里巴巴有4名员工却因为抢月饼遭开除。。。。", ViewCount = 146 });
            list.Add(new BlogInfo() { Id = 4, BlogTitle = "快播涉黄案宣判：CEO王欣获刑3年6个月", BlogContent = "9月13日上午消息，北京市海淀区法院今日上午对深圳市快播科技有限公司，被告人王欣、吴铭、张克东、牛文举涉嫌传播淫秽物品牟利罪一案进行公开宣判。。。。", ViewCount = 803 });

            //建立Lucene索引文件
            string indexPath = AppDomain.CurrentDomain.BaseDirectory+"indexFile";
            sw.Start();
            IndexWriter writer = new IndexWriter(FSDirectory.Open(indexPath), new Lucene.Net.Analysis.PanGu.PanGuAnalyzer(), true, IndexWriter.MaxFieldLength.LIMITED);
            foreach (BlogInfo item in list)
            {
                Document doc = new Document();
                Field blogId = new Field("BlogId", item.Id.ToString(), Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.NO);
                Field blogTitle = new Field("BlogTitle", item.BlogTitle.ToString(), Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.NO);
                Field blogContent = new Field("BlogContent", item.BlogContent.ToString(), Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.NO);
                Field viewCount = new Field("ViewCount", item.ViewCount.ToString(), Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.NO);
                doc.Add(blogId);
                doc.Add(blogTitle);
                doc.Add(blogContent);
                doc.Add(viewCount);
                writer.AddDocument(doc);
            }
            writer.Optimize();
            writer.Commit();
            sw.Stop();
            Console.Write("本次建立索引数=" + list.Count + "  所花时间= " + sw.Elapsed);
            Console.ReadLine();

        }
    }
    class BlogInfo
    {
        public int Id { get; set; }
        public string BlogTitle { get; set; }
        public string BlogContent { get; set; }
        public int ViewCount { get; set; }
    }
}
