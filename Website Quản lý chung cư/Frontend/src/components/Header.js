import { Typography, Box } from "@mui/material";
const Header = ({ title }) => {
  return (
    <Box mb="30px">
      <Typography
        variant="h2"
        fontWeight="bold"
        sx={{ m: "0 0 2px 0" }}
      >
        {title}
      </Typography>
    </Box>
  );
};
export default Header;
