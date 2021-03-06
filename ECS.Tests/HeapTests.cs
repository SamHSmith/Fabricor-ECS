using System;
using NUnit.Framework;
using Fabricor.ECS;

namespace ECS.Tests
{
    public class HeapTests{

        [Test]
        [TestCase(1024L*1024)]
        public void CreationAndDeletion(long size){
            EntityHeap heap=new EntityHeap(size);

            heap.Free();
        }

        [Test]
        public void Appending(){
            EntityHeap heap=new EntityHeap(32);

            heap.AppendEntities(new Type[0],4);
            Assert.AreEqual(8,heap.HeapLength,0.1);

            heap.Free();
        }

        [Test]
        public void Workloads(){
            EntityHeap heap=new EntityHeap(32);
            heap.AppendEntities(new Type[0],4);
            SystemWorkload[] workloads=heap.GetSystemWorkloads(2);
            Assert.AreEqual(0,workloads[0].startAddress,0.1);
            Assert.AreEqual(4,workloads[0].endAddress,0.1);

            Assert.AreEqual(4,workloads[1].startAddress,0.1);
            Assert.AreEqual(8,workloads[1].endAddress,0.1);
            heap.Free();

            heap=new EntityHeap(32);
            heap.AppendEntities(new Type[0],4);
            workloads=heap.GetSystemWorkloads(3);
            Assert.AreEqual(0,workloads[0].startAddress,0.1);
            Assert.AreEqual(2,workloads[0].endAddress,0.1);

            Assert.AreEqual(2,workloads[1].startAddress,0.1);
            Assert.AreEqual(4,workloads[1].endAddress,0.1);

            Assert.AreEqual(4,workloads[2].startAddress,0.1);
            Assert.AreEqual(8,workloads[2].endAddress,0.1);
            heap.Free();
        }
    }
}