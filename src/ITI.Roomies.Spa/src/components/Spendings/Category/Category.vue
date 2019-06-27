<template>
  <div>
    <el-row :gutter="20">
      <el-col :xs="4" :sm="6" :md="8" :lg="9" :xl="11">
        <el-card>
          <div>
            <CategoryCreateEdit></CategoryCreateEdit>
          </div>
        </el-card>
      </el-col>

      <el-col :xs="4" :sm="6" :md="8" :lg="9" :xl="11">
        <el-card>
          <div>
            <CategoryList></CategoryList>
          </div>
        </el-card>
      </el-col>
    </el-row>
  </div>
</template>

<script>
import CategoryList from "../../Spendings/Category/CategoryList";
import CategoryCreateEdit from "../../Spendings/Category/CategoryCreateEdit";

export default {
  components: {
    CategoryList,
    CategoryCreateEdit
  }
};
</script>


<style lang="scss" scoped>
.el-col {
  border-radius: 4px;
}
.bg-purple-dark {
  background: #99a9bf;
}
.bg-purple {
  background: #d3dce6;
}
.bg-purple-light {
  background: #e5e9f2;
}
.grid-content {
  border-radius: 4px;
  min-height: 36px;
}
.text {
  font-size: 14px;
}

.item {
  padding: 18px 0;
}

.box-card {
  width: 480px;
}
</style>

