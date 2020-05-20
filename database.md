### Oracle执行计划

#### 执行计划的常用列字段解释：

- 基数（Rows）：Oracle估计的当前操作的返回结果集行数

- 字节（Bytes）：执行该步骤后返回的字节数

- 耗费（COST）、CPU耗费：Oracle估计的该步骤的执行成本，用于说明SQL执行的代价，理论上越小越好（该值可能与实际有出入）

- 时间（Time）：Oracle估计的当前操作所需的时间

#### 执行顺序

根据Operation缩进来判断，缩进最多的最先执行；（缩进相同时，最上面的最先执行）
例：上图中 INDEX RANGE SCAN 和 INDEX UNIQUE SCAN 两个动作缩进最多，最上面的 INDEX RANGE SCAN 先执行；
同一级如果某个动作没有子ID就最先执行
同一级的动作执行时遵循最上最右先执行的原则
例：上图中 TABLE ACCESS BY GLOBAL INDEX ROWID 和 TABLE ACCESS BY INDEX ROWID 两个动作缩进都在同一级，则位于上面的 TABLE ACCESS BY GLOBAL INDEX ROWID 这个动作先执行；这个动作又包含一个子动作 INDEX RANGE SCAN，则位于右边的子动作 INDEX RANGE SCAN 先执行；
图示中的SQL执行顺序即为：
INDEX RANGE SCAN  —>  TABLE ACCESS BY GLOBAL INDEX ROWID  —>  INDEX UNIQUE SCAN  —>  TABLE ACCESS BY INDEX ROWID  —>  NESTED LOOPS OUTER  —>  SORT GROUP BY  —>  SELECT STATEMENT, GOAL = ALL_ROWS

#### 表访问的几种方式

1. TABLE ACCESS FULL（全表扫描）：

Oracle会读取表中所有的行，并检查每一行是否满足SQL语句中的 Where 限制条件；
全表扫描时可以使用多块读（即一次I/O读取多块数据块）操作，提升吞吐量；
使用建议：数据量太大的表不建议使用全表扫描，除非本身需要取出的数据较多，占到表数据总量的 5% ~ 10% 或以上

2. TABLE ACCESS BY ROWID（通过ROWID的表存取）：

ROWID是由Oracle自动加在表中每行最后的一列伪列，既然是伪列，就说明表中并不会物理存储ROWID的值；
你可以像使用其它列一样使用它，只是不能对该列的值进行增、删、改操作；
一旦一行数据插入后，则其对应的ROWID在该行的生命周期内是唯一的，即使发生行迁移，该行的ROWID值也不变。
让我们再回到 TABLE ACCESS BY ROWID 来：
行的ROWID指出了该行所在的数据文件、数据块以及行在该块中的位置，所以通过ROWID可以快速定位到目标数据上，这也是Oracle中存取单行数据最快的方法

3. TABLE ACCESS BY INDEX SCAN（索引扫描）：

- 在索引块中，既存储每个索引的键值，也存储具有该键值的行的ROWID。
索引扫描其实分为两步：
Ⅰ：扫描索引得到对应的ROWID
Ⅱ：通过ROWID定位到具体的行读取数据

- 索引扫描细分：

1. INDEX UNIQUE SCAN（索引唯一扫描）
2. INDEX RANGE SCAN（索引范围扫描）
3. INDEX FULL SCAN（索引全扫描）
4. INDEX FAST FULL SCAN（索引快速扫描）
5. INDEX SKIP SCAN（索引跳跃扫描）

........(未完)