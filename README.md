# XiechengTravelApi
一个模仿携程旅游网站的REST-full风格的WebApi，使用jwt单点登录用户验证方式
目前实现了：
</p>
一、授权验证部分：
    </p> 1.Jwt用户登录，用户注册

二、业务处理部分：</p>
</p>（1）旅游路线资源
   </p>  1.取得所有旅游路线 
    </p>2.根据QueryString中的指定参数条件进行筛选，分页，并在header附加导航页信息
    </p>3.根据指定路线Id获取指定的旅游路线信息，并一同返回了旅游路线的子资源（旅游路线图）
    </p>4.创建旅游路线资源，同时可以提交子资源
    </p>5.整体更新数据
    </p>6.部分更新数据
    </p>7.删除指定路线数据
    </p>8.创建/删除子数据路线图

</p>（2）购物车实现</p>
   </p>  1.取得当前用户购物车中的商品
    </p> 2.为购物车添加商品
    </p> 3.从购物车中删除商品
    </p> 4.清空购物车
    </p> 5.提交订单

</p>（3）订单系统</p>
   </p> 1.取得该用户的所有订单
     </p> 2.获得指定订单详细信息
     </p> 3.删除指定订单
     </p> 4.删除所有订单
