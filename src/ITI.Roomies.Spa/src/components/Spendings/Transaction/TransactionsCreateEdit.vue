<template>
  <div>
    <div>
      <h1>Ajouter une Transaction</h1>
    </div>

    <div>
      <el-tabs type="border-card">
        
      <el-tab-pane label="Category">Roomie
         <TDepenseCreateEdit></TDepenseCreateEdit>
      </el-tab-pane>

      <el-tab-pane label="Roomie">Category
        <TBudgetCreateEdit></TBudgetCreateEdit>
      </el-tab-pane>

      </el-tabs>
    </div>
    
  </div>
</template>

<script>

import TDepenseCreateEdit from "../Transaction/TDepenseCreateEdit.vue";
import TBudgetCreateEdit  from "../Transaction/TBudgetCreateEdit.vue";

export default {
  components: {
    TBudgetCreateEdit, 
    TDepenseCreateEdit,
  }  
};
</script>


