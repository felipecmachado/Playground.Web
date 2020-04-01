<template>
    <div class="row">
      <div class="col-12">
        <card  style="width:800px;" title="My Statement" subTitle="All transactions in the last 30 days">
          <div  style="width:800px;" slot="raw-content" class="table">
            <paper-table :data="statement.data" :columns="statement.columns">

            </paper-table>
          </div>
        </card>
      </div>

    </div>
</template>
<script>
import { PaperTable } from "@/components";
import axios from 'axios';
export default {
  created() {

    axios
      .get('http://localhost:5000/api/accounts/1/statement', { headers: { Authorization: localStorage.getItem('token') }})
      .then(response => 
      {
        this.statement.data = (
          response.data
          .reverse()
          .map(function(x){
            
            switch (x.transactionType) {
              case 0:
                x.transactionType = "Withdraw";
                break;
              case 1:
                x.transactionType = "Deposit";
                break;
              case 2:
                x.transactionType = "Payment";
                break;
              case 3:
                x.transactionType = "Transfer";
                break;
            }

            return {
              timestamp: x.timestamp,
              transaction: x.transactionType,
              amount: x.amount,
              message: x.message
            };
          })
        );

      });
  },
  components: {
    PaperTable
  },
  data() {
    return {
      statement: {
        columns: ["Timestamp", "Transaction", "Amount", "Message"],
        data: null
      }
    };
  }
};
</script>
<style>
</style>
