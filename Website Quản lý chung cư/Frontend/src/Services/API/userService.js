import axios from "../../setups/custom_axios";
const getAllUsers = () => {
  return axios.get("/get-all-users");
};
const createUser = (payload) => {
  const { username, firstName, lastName, email, role } = payload;
  return axios.post("/user", {
    UserName: username,
    GivenName: firstName,
    SurName: lastName,
    Email: email,
    RoleId: +role,
  });
};
const updateUser = (payload) => {
  const { userName, firstName, lastName, email, role } = payload;
  return axios.put("/user", {
    userName: userName,
    givenName: firstName,
    surName: lastName,
    email: email,
    RoleId: +role,
  });
};
const deleteUser = (userName) => {
  return axios.delete(`/user?userName=${userName}`);
};
const userService = { getAllUsers, createUser, updateUser, deleteUser };
export default userService;
