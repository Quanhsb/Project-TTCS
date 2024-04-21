import axios from "axios";
const instance = axios.create({
  baseURL: process.env.REACT_APP_BASE_URL,
  headers: {
    "Content-Type": "application/json",
  },
});
instance.interceptors.request.use(
  function (config) {
    const token = localStorage.getItem("accessToken");
    config.headers.Authorization = token ? `Bearer ${token}` : "";
    return config;
  },
  function (error) {
    return Promise.reject(error);
  }
);
instance.interceptors.response.use(
  function (response) {
    return response.data;
  },
  function (error) {
    const status = error.response?.status || 500;
    switch (status) {
      case 401: {
        return error.response.data;
      }
      case 403: {
        return Promise.reject(error);
      }
      case 400: {
        return Promise.reject(error);
      }
      case 404: {
        return Promise.reject(error);
      }
      case 409: {
        return Promise.reject(error);
      }
      case 422: {
        return Promise.reject(error);
      }
      default: {
        return Promise.reject(error);
      }
    }
  }
);
export default instance;
