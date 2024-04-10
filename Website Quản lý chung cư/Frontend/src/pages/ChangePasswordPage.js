import React, { useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import axios from '../setups/custom_axios';
import { logout } from "../Redux/authSlice";
import { Button } from "@mui/material";
import { toast } from "react-toastify";
export default function ChangePasswordPage() {
  const dispatch = useDispatch();
  const [oldPassword, setOldPassword] = useState('');
  const [newPassword, setNewPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const { user } = useSelector(
    (state) => state.auth
  );
  const handleSubmit = async (event) => {
    event.preventDefault();
    if (newPassword !== confirmPassword) {
      toast('Xác nhận mật khẩu mới không đúng');
      return;
    }
    try {
      var res = await axios.put('/change-password', {
        userName: user?.userName,
        oldPassword,
        newPassword
      });
      toast(res?.message);
      dispatch(logout());
    }
    catch (e) {
      toast(e?.response?.data?.reason || e?.response?.data?.message || "Có lỗi xảy ra");
      return;
    }
  }
  return (
    <div>
      <div className="input-custome" style={{ margin: "auto", width: '40%' }}>
        <div className="text-center mt-3 mb-3" >
          <h3>Đổi mật khẩu</h3>
        </div>
        <form className="content-body row" onSubmit={handleSubmit}>
          <div className="form-group">
            <label>
              Tên đăng nhập (<span className="text-danger">*</span>)
            </label>
            <input
              type="text"
              className="form-control"
              value={user?.userName}
              disabled
            />
          </div>
          <div className="form-group">
            <label>
              Mật khẩu cũ (<span className="text-danger">*</span>)
            </label>
            <input
              type="password"
              className="form-control"
              value={oldPassword}
              required
              minLength={5}
              onChange={(e) => setOldPassword(e.target.value)}
              placeholder="Nhập mật khẩu cũ . . ."
            />
          </div>
          <div className="form-group">
            <label>
              Mật khẩu mới (<span className="text-danger">*</span>)
            </label>
            <input
              type="password"
              className="form-control"
              value={newPassword}
              required
              minLength={5}
              onChange={(e) => setNewPassword(e.target.value)}
              placeholder="Nhập mật khẩu mới . . ."
            />
          </div>
          <div className="form-group">
            <label>
              Xác nhận mật khẩu mới (<span className="text-danger">*</span>)
            </label>
            <input
              type="password"
              className="form-control"
              value={confirmPassword}
              required
              minLength={5}
              onChange={(e) => setConfirmPassword(e.target.value)}
              placeholder="Xác nhận mật khẩu mới . . ."
            />
          </div>
          <Button type="submit" variant="contained" color="info"
            style={{ width: 100, margin: 'auto' }}>Gửi</Button>
        </form>
      </div>
    </div>
  );
}