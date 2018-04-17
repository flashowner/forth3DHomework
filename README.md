### 打飞碟(Hit UFO)游戏<br>

  这次的作业是编写一个简单的打飞碟游戏，在这个游戏中我首先设定飞碟的总数是10个这样一回合飞碟的数量不会太多以免会有些枯燥乏味。不过相比于之前的游戏物体，
飞碟具备一些独特的性质，首先它在被击中后会被销毁，就算没有击中飞碟，等到飞碟飞到看不见的地方也要将它销毁不然会占用游戏资源。而每当有新的需求时需要实例
化一个飞碟，照这种情况来看如果在飞碟需要生成的时候实例化，在需要销毁时销毁会占用游戏大量的运行时间，很容易会出现游戏画质下降的现象或者是游戏卡顿的现象
。为了解决这个问题就需要使用工厂模式。工厂顾名思义在这个游戏中就是用来负责生产飞碟和管理飞碟的回收的类，为了避免资源的浪费可以声明两个List used
和unuesd，一个用来放还没使用过的飞碟，一个用来放使用过的飞碟，为了节约资源，在这里被击中的飞碟不进行销毁而是将它放到玩家看不见的地方。在需要使用飞碟时
先从unused找看unused中有没有飞碟，如果有直接拿去用，如果没有就新实例化一个，然后将该飞碟放到used中，并将该飞碟从unused中移除。在回收飞碟时将used中的
所有飞碟重新放入unused中，然后将其从used中移除，这样就可以实现资源的循环利用，能够增加运行的效率。具体的实现代码如下：<br>
![](https://github.com/flashowner/forth3DHomework/blob/master/%E6%88%AA%E5%9B%BE/%E6%8D%95%E8%8E%B7.PNG)<br>
  首先先从Prefabs中取UFO模型赋值给ufomodel方便接下来的使用。<br>
![](https://github.com/flashowner/forth3DHomework/blob/master/%E6%88%AA%E5%9B%BE/%E6%8D%95%E8%8E%B71.PNG) <br>
接着就是从unused中取飞碟<br>
![](https://github.com/flashowner/forth3DHomework/blob/master/%E6%88%AA%E5%9B%BE/%E6%8D%95%E8%8E%B72.PNG) <br>
然后是回收飞碟的过程<br>
在这个游戏中还有一个非常重要的一点便是飞碟的设计，因为整个游戏便是围绕飞碟而进行的，在这里参考unity中游戏对象和组件的关系，其实可以把飞碟一些常用的属性
例如颜色，方向，大小，速度作为一个组件，等到实例化飞碟对象时挂到这个游戏对象上。从上面工厂生产飞碟的代码中可以看到，在实例化飞碟的时候其实我是添加了一个
叫做UFOInfo的组件到飞碟上的，而这个组件就是包含了飞碟的一些属性，具体声明如下：<br>
![](https://github.com/flashowner/forth3DHomework/blob/master/%E6%88%AA%E5%9B%BE/%E6%8D%95%E8%8E%B73.PNG)<br>
到当前为止一个最基本的飞碟模型包括生产和回收的过程就完成了，飞碟既然生产出来接下来就要考虑游戏规则对飞碟的影响了。本游戏的规则是一共有3个关卡，第一个
关卡的飞碟是红色的，当然速度是比较慢的，而且生成的位置波动幅度较小，如果打中就会获得1分，第二个关卡的飞碟是蓝色的，速度稍微快了一些，打中就会获得2分，
第三个关卡的飞碟是绿色的，速度最快，而且出现位置是波动幅度最大的，为了使得飞碟稍微好打一些给飞碟加了一个向下的相当于重力10的加速度。一旦获得10分那么
游戏结束玩家获胜，如果三个关卡都过了还不够分数游戏会从第一关开始，就这样一直循环下去直到玩家获胜。所以当控制器要求从工厂中拿飞碟时需要提供当前的游戏
关数，这样工厂可以根据关卡数完善不同的飞碟，具体实现代码如下：<br>
![](https://github.com/flashowner/forth3DHomework/blob/master/%E6%88%AA%E5%9B%BE/%E6%8D%95%E8%8E%B74.PNG)<br>
![](https://github.com/flashowner/forth3DHomework/blob/master/%E6%88%AA%E5%9B%BE/%E6%8D%95%E8%8E%B75.PNG)<br>
这样一个飞碟就生成了，然后需要考虑的是飞碟的动作，因为这个游戏是动作分离版本的，所以飞碟的动作和飞碟的属性并不在一起，因为这是个简单的射飞碟游戏，所以
飞碟的动作相当简单，第一个是在飞碟生成的时候赋予它一个速度，在这里飞碟有两个方向的速度，一个是重力方向的速度，还有一个是沿着飞碟方向的速度，这个动作必
须在飞碟产生的时候完成。其次是当飞碟飞到一定位置时需要让它“销毁”，也就是说使它的activeSelf设为false，然后将它移到一个看不见的地方避免再次被鼠标点到。
具体实现的代码如下：<br>
![](https://github.com/flashowner/Picture0/blob/master/%E6%88%AA%E5%9B%BE0/%E6%8D%95%E8%8E%B76.PNG)<br>
![](https://github.com/flashowner/Picture0/blob/master/%E6%88%AA%E5%9B%BE0/%E6%8D%95%E8%8E%B77.PNG)<br>
要完成整个游戏最后还需要有击中飞碟这个动作，这里我使用鼠标发出射线，击中飞碟后飞碟消失：<br>
<pre>
 public void hitObject(Vector3 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);

        RaycastHit[] objects;
        objects = Physics.RaycastAll(ray);
        for  (int i = 0; i < objects.Length; i++)
        {
            RaycastHit hit = objects[i];

            if (hit.collider.gameObject.GetComponent<UFOInfo>() != null)
            {
                scoreRuler.Compute(hit.collider.gameObject);
                hit.collider.gameObject.transform.position = new Vector3(0, -4, 0);
            }
        }
    }
 </pre> <br>
剩下具体的实现步骤可以查看我的代码:<br>
![快速通道](https://github.com/flashowner/forth3DHomework/tree/master/Scritps)<br>
这是游戏视频的地址http://v.youku.com/v_show/id_XMzU0NTc4Mjg5Mg==.html?spm=a2h3j.8428770.3416059.1

