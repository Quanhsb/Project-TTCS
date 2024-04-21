import axios from "../../setups/custom_axios";
const login = async (userName, password) => {
  return axios.post("/login", { userName, password });
};
const checkToken = async () => {
  return await axios.get("/verify-token");
};
const logout = () => {
  return axios.post("/logout");
};
const AuthService = { checkToken, login, logout };
export default AuthService;
