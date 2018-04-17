### 打飞碟(Hit UFO)游戏<br>

  这次的作业是编写一个简单的打飞碟游戏，在这个游戏中我首先设定飞碟的总数是10个这样一回合飞碟的数量不会太多以免会有些枯燥乏味。不过相比于之前的游戏物体，
飞碟具备一些独特的性质，首先它在被击中后会被销毁，就算没有击中飞碟，等到飞碟飞到看不见的地方也要将它销毁不然会占用游戏资源。而每当有新的需求时需要实例
化一个飞碟，照这种情况来看如果在飞碟需要生成的时候实例化，在需要销毁时销毁会占用游戏大量的运行时间，很容易会出现游戏画质下降的现象或者是游戏卡顿的现象
。为了解决这个问题就需要使用工厂模式。工厂顾名思义在这个游戏中就是用来负责生产飞碟和管理飞碟的回收的类，为了避免资源的浪费可以声明两个List used
和unuesd，一个用来放还没使用过的飞碟，一个用来放使用过的飞碟，为了节约资源，在这里被击中的飞碟不进行销毁而是将它放到玩家看不见的地方。在需要使用飞碟时
先从unused找看unused中有没有飞碟，如果有直接拿去用，如果没有就新实例化一个，然后将该飞碟放到used中，并将该飞碟从unused中移除。在回收飞碟时将used中的
所有飞碟重新放入unused中，然后将其从used中移除，这样就可以实现资源的循环利用，能够增加运行的效率。具体的实现代码如下：<br>
