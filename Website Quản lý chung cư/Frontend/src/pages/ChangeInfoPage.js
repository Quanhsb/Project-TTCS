import React, { useState, useEffect } from "react";
import axios from '../setups/custom_axios';
import { useDispatch, useSelector } from "react-redux";
import { loadUser } from "../Redux/authSlice";
import { Button } from "@mui/material";
import { toast } from "react-toastify";
export default function ChangeInfoPage() {
  const dispatch = useDispatch();
  const [firstName, setFirstName] = useState();
  const [lastName, setLastName] = useState();
  const [email, setEmail] = useState();
  const { user } = useSelector(
    (state) => state.auth
  );
  const handleSubmit = async (event) => {
    event.preventDefault();
    try {
      var res = await axios.put('/user', {
        "userName": user.userName,
        "firstName": firstName.trim(),
        "lastName": lastName.trim(),
        "email": email.trim(),
        "roleId": user.roleId,
        "userUpdate": user.userName,
        "version": user.version
      });
      toast(res?.message);
      dispatch(loadUser());
    }
    catch (e) {
      toast(e?.response?.data?.reason || e?.response?.data?.message || "Có lỗi xảy ra");
      return;
    }
  }
  useEffect(() => {
    setFirstName(user?.firstName);
    setLastName(user?.lastName);
    setEmail(user?.email);
  }, [user]);
  return (
    <div>
      <div className="input-custome" style={{ margin: "auto", width: '40%' }}>
        <div className="text-center mt-3 mb-3" >
          <h3>Cập nhật thông tin</h3>
        </div>
        <form className="content-body row" onSubmit={handleSubmit}>
          <div className="form-group">
            <label>
              Tên (<span className="text-danger">*</span>)
            </label>
            <input
              type="text"
              onChange={(e) => setFirstName(e.target.value)}
              className="form-control"
              required
              value={firstName}
            />
          </div>
          <div className="form-group">
            <label>
              Họ (<span className="text-danger">*</span>)
            </label>
            <input
              type="text"
              onChange={(e) => setLastName(e.target.value)}
              className="form-control"
              required
              value={lastName}
            />
          </div>
          <div className="form-group">
            <label>
              Email (<span className="text-danger">*</span>)
            </label>
            <input
              type="text"
              className="form-control"
              value={email}
              required
              onChange={(e) => setEmail(e.target.value)}
              placeholder="Nhập email . . ."
            />
          </div>
          <Button type="submit" variant="contained" color="info"
            style={{ width: 100, margin: 'auto' }}>Gửi</Button>
        </form>
      </div>
    </div>
  );
}
