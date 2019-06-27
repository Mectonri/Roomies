<template>
  <div>
    <div>
      <h1>Ajouter une Transaction</h1>
    </div>

    <div>
      <el-tabs type="border-card">
        
      <el-tab-pane label="Category">Roomie
         <TDepenseCreate></TDepenseCreate>
      </el-tab-pane>

      <el-tab-pane label="Roomie">Category
        <TBudgetCreate></TBudgetCreate>
      </el-tab-pane>

      </el-tabs>
    </div>
    
  </div>
</template>

<script>

import TDepenseCreate from "../Transaction/TDepenseCreate";
import TBudgetCreate  from "../Transaction/TBudgetCreate";

export default {
  components: {
    TBudgetCreate, 
    TDepenseCreate,
  }  
};
</script>


