<template>
  <card class="card col-md-9" title="My Checking Account">
    <div>
      <form @submit.prevent>

        <div class="row">
          <div class="col-md-6">
            <fg-input type="text"
                      label="First Name"
                      placeholder="First Name"
                      v-model="user.firstName">
            </fg-input>
          </div>
          <div class="col-md-6">
            <fg-input type="text"
                      label="Last Name"
                      placeholder="Last Name"
                      v-model="user.lastName">
            </fg-input>
          </div>
        </div>

        <div class="row">
          <div class="col-md-4">
            <fg-input type="text"
                      label="Login"
                      placeholder="Login"
                      :disabled="true"
                      v-model="user.login">
            </fg-input>
          </div>
          <div class="col-md-4">
            <fg-input type="text"
                      label="Password"
                      placeholder="Password"
                      :disabled="true"
                      v-model="user.password">
            </fg-input>
          </div>
          <div class="col-md-4">
            <fg-input type="text"
                      label="Phone Number"
                      placeholder="Phone Number"
                      :disabled="true"
                      v-model="user.phoneNumber">
            </fg-input>
          </div>
        </div>

        <div class="row">
          <div class="col-md-4">
            <fg-input type="text"
                      label="Account Number"
                      placeholder="Account Number"
                      :disabled="true"
                      v-model="user.accountNumber">
            </fg-input>
          </div>
          <div class="col-md-4">
            <fg-input type="text"
                      label="Transaction Token"
                      placeholder="Transaction Token"
                      :disabled="true"
                      v-model="user.transactionToken">
            </fg-input>
          </div>
          <div class="col-md-4">
            <fg-input type="text"
                      label="Last Accessed at"
                      placeholder="Last Accessed at"
                      :disabled="true"
                      v-model="user.lastAccessedAt">
            </fg-input>
          </div>
        </div>

        <div class="clearfix"></div>
      </form>
    </div>
  </card>
</template>
<script>
import axios from 'axios';
export default {
  created() {

    axios
      .get('http://localhost:5000/api/users/me', { headers: { Authorization: localStorage.getItem('token') }})
      .then(response => 
      {
        this.user = {
        firstName: response.data.item.firstName,
        lastName: response.data.item.lastName,
        login: response.data.item.login,
        password: response.data.item.password,
        emailAddress: response.data.item.emailAddress,
        phoneNumber: response.data.item.phoneNumber,
        accountNumber: response.data.item.checkingAccount.accountNumber,
        transactionToken: response.data.item.checkingAccount.transactionToken,
        lastAccessedAt: response.data.item.lastAccessAt
      };
      });

      this.$forceUpdate();
  },
  data() {
    return {
      user: {
        firstName: "",
        lastName: "",
        emailAddress: "",
        login: "",
        password: "",
        accountNumber: "",
        transactionToken: "",
        lastAccessedAt: ""
      }
    };
  },
  methods: {
    updateProfile() {
      alert("Your data: " + JSON.stringify(this.user));
    }
  }
};
</script>
<style>
</style>
