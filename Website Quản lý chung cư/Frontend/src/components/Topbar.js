import { Box } from "@mui/material";
import { Link } from 'react-router-dom';
import Dropdown from 'react-bootstrap/Dropdown';
import { logout } from "../Redux/authSlice";
import { useDispatch, useSelector } from "react-redux";
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
const Topbar = () => {
  const dispatch = useDispatch();
  const { user, isAuthenticated } = useSelector((state) => state.auth);
  return (
    <div style={{ position: 'absolute', top: 10, right: 10 }}>
      <Box display="flex">
        {user && isAuthenticated === true ?
          (
            <Dropdown>
              <Dropdown.Toggle style={{ backgroundColor: '#0288d1' }} id="dropdown-basic">
                {<AccountCircleIcon />}
              </Dropdown.Toggle>
              <Dropdown.Menu>
                <Dropdown.Item href="/change-info">Cập nhật thông tin</Dropdown.Item>
                <Dropdown.Item href="/change-password">Đổi mật khẩu</Dropdown.Item>
                <Dropdown.Item onClick={() => {
                  dispatch(logout());
                }}>
                  <span>Đăng xuất</span>
                </Dropdown.Item>
              </Dropdown.Menu>
            </Dropdown>
          ) : (
            <Link to="/login" className="nav-link">Login
            </Link>
          )}
      </Box>
    </div>
  );
};
export default Topbar;
