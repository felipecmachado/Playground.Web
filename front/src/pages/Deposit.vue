<template>
  <card class="card col-md-9" title="Make a deposit to your account">
    <div>
      <form @submit="formSubmit">
        <div class="row">
          <div class="col-md-6">
            <fg-input type="text"
                      label="Transaction Type"
                      :disabled="true"
                      placeholder="Deposit"
                      value="DEPOSIT">
            </fg-input>
          </div>
          <div class="col-md-6">

            <fg-input type="text"
                      label="Transaction Token"
                      placeholder="#######"
                      v-model="token">
            </fg-input>
          </div>

        </div>

        <div class="row">
          <div class="col-md-12">
            <fg-input type="number"
                      label="Amount"
                      v-model="amount"
                      placeholder="Digit the amount here">
            </fg-input>
          </div>
        </div>

        <div class="text-left">
          <p-button type="info"
                    round
                    @click.native.prevent="formSubmit">
            Deposit
          </p-button>
        </div>
        {{output}}
        <div class="clearfix"></div>
      </form>
    </div>
  </card>
</template>
<script>
import axios from 'axios';
export default {
    data() {
        return {
            amount: '0.00',
            token: '',
            output: ''
        };
    },
    methods: {
        formSubmit(e) {
            e.preventDefault();
            let currentObj = this;

            axios
            .post('http://localhost:5000/api/transactions/deposit',
            {
                "checkingAccountId": 1,
                "transactionToken": this.token,
                "amount": this.amount
            }, { headers: { Authorization: localStorage.getItem('token') }}
            ).then(function (response) {
                currentObj.output = "Success";
            })
            .catch(function (error) {
                currentObj.output = "Invalid token";
            });
        }
    }
};
</script>
<style>
</style>
