import { defineStore } from "pinia";
import api from "../services/api";
import jwtDecode from "jwt-decode";

export const useAuthStore = defineStore("auth", {
    state: () => ({
    token: localStorage.getItem("token") || "",
    user: JSON.parse(localStorage.getItem("user") || "null") || {},
  }),

  actions: {

    // ✅ LOGIN
    async login(email, password) {
      const baseURL = "https://localhost:53954";

      const res = await api.post(`${baseURL}/api/auth/login`, {
        email,
        password,
      });

      this.token = res.data.token;

      // ✅ IMPORTANT: use API user object, NOT jwt decode
      this.user = res.data.user;

      localStorage.setItem("token", this.token);
      localStorage.setItem("user", JSON.stringify(this.user));
    },

    async getUsersByManagerId(managerId) {

      const baseURL = "https://localhost:53954";
      //https://localhost:53954/api/users/manager/2
      
      const res = await api.get(
        `${baseURL}/api/users/manager/${managerId}`,
        {
          headers: {
            Authorization: `Bearer ${this.token}`,
          },
        }
      );

      return res.data;
    },

    // ✅ REGISTER (NEW METHOD ADDED)
    async register(payload) {

      const baseURL = "https://localhost:53954";
      console.log(baseURL);

      const res = await api.post(`${baseURL}/api/auth/register`, payload);

      // optional: return response to component
      return res.data;
    },

      // CREATE TASK
    async createTask(payload) {

      const baseURL = "https://localhost:53954";

      const res = await api.post(
        `${baseURL}/api/tasks`,
        payload,
        {
          headers: {
            Authorization: `Bearer ${this.token}`,
          },
        }
      );

      return res.data;
    },

    async getUsers() {
      const baseURL = "https://localhost:53954";

      const res = await api.get(`${baseURL}/api/users`, {
        headers: {
          Authorization: `Bearer ${this.token}`,
        },
      });

      return res.data;
    },

    // ✅ LOGOUT
    logout() {
      this.token = "";
      this.user = {};

      localStorage.removeItem("token");
      localStorage.removeItem("user");
    },
  },
});