<template>
  <div class="wrapper">
    <side-bar>
      <template slot="links">
        <sidebar-link to="/dashboard" name="Dashboard" icon="ti-home"/>
        <sidebar-link to="/account" name="My Account" icon="ti-user"/>
        <sidebar-link to="/statement" name="History" icon="ti-archive"/>
        <sidebar-link to="/transfer" name="Transfer" icon="ti-email"/>
        <sidebar-link to="/deposit" name="Deposit" icon="ti-import"/>
        <sidebar-link to="/withdraw" name="Withdraw" icon="ti-export"/>
        <sidebar-link to="/payment" name="Payment" icon="ti-money"/>
        <sidebar-link to="/settings" name="Global Settings" icon="ti-settings"/>
        <sidebar-link to="/status" name="Status" icon="ti-heart"/>
      </template>

    </side-bar>
    <div class="main-panel">
      <top-navbar></top-navbar>

      <dashboard-content @click.native="toggleSidebar">

      </dashboard-content>

    </div>
  </div>
</template>
<style lang="scss">
</style>
<script>
import TopNavbar from "./TopNavbar.vue";
import DashboardContent from "./Content.vue";
import MobileMenu from "./MobileMenu";
export default {
  components: {
    TopNavbar,
    DashboardContent,
    MobileMenu
  },
  methods: {
    toggleSidebar() {
      if (this.$sidebar.showSidebar) {
        this.$sidebar.displaySidebar(false);
      }
    }
  }
};
</script>
