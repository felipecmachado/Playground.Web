<template>
  <card class="card col-md-9" title="Pay a Bill">
    <div>
      <form @submit="formSubmit">
        <div class="row">
          <div class="col-md-6">
            <fg-input type="text"
                      label="Transaction Type"
                      :disabled="true"
                      placeholder="Payment"
                      value="PAYMENT">
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
                      label="Barcode"
                      v-model="barCode"
                      placeholder="barCode">
            </fg-input>
          </div>
        </div>

        <div class="row">
          <div class="col-md-6">
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
            Send payment
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
            barCode: '',
            output: ''
        };
    },
    methods: {
        formSubmit(e) {
            e.preventDefault();
            let currentObj = this;

            axios
            .post('http://localhost:5000/api/transactions/payment',
            {
                "checkingAccountId": 1,
                "transactionToken": this.token,
                "barCode": this.barCode,
                "amount": this.amount
            }, { headers: { Authorization: localStorage.getItem('token') }}
            ).then(function (response) {
                currentObj.output = "Success";
            })
            .catch(function (error) {
                currentObj.output = error.response.data.responseStatus.errors[0].message;
            });
        }
    }
};
</script>
<style>
</style>
