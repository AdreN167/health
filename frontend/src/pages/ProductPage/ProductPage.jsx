import { Button, Stack, TextField } from "@mui/material";
import AdminLayout from "../../components/AdminLayout/AdminLayout";
import { useContext, useEffect, useState } from "react";
import useInput from "../../hooks/useInput";
import { UploadFile } from "@mui/icons-material";
import UploadButton from "../../components/UploadButton/UploadButton";
import { getProducts } from "../../api/productService";
import AuthContext from "../../store/AuthContext";
import FullFeaturedCrudGrid from "../../components/Test/Test";

const errMsg = "Ошибка ввода";

const validateNumber = (num) =>
  num > 0 ? { isValid: true, err: null } : { isValid: false, err: errMsg };

const ProductPage = () => {
  const { token } = useContext(AuthContext);

  const product = useInput("Продукт1", (str) =>
    str !== "" && str !== " " && str !== null
      ? { isValid: true, err: null }
      : { isValid: false, err: errMsg }
  );
  const calories = useInput("1", validateNumber);
  const proteins = useInput("1", validateNumber);
  const fats = useInput("1", validateNumber);
  const carbo = useInput("1", validateNumber);
  const file = useInput(null, (file) =>
    file.type.split("/")[0] !== "image"
      ? { isValid: false, err: "Нужна картинка" }
      : { isValid: true, err: null }
  );

  const [fetcedProducts, setFetchedProducts] = useState([]);

  useEffect(() => {
    const get = async () => {
      try {
        const data = await getProducts(token);
        setFetchedProducts(data);
      } catch (err) {
        console.log(err);
      }
    };
    get();
  }, []);

  return (
    <AdminLayout>
      <form>
        <Stack direction="row" sx={{ gap: 2 }}>
          <TextField
            label="Название"
            value={product.value}
            onChange={product.onchange}
            error={product.error !== null}
            helperText={product.error}
          />
          <TextField
            label="Калории"
            type="number"
            sx={{ maxWidth: 120 }}
            value={calories.value}
            onChange={calories.onchange}
            error={calories.error !== null}
            helperText={calories.error}
          />
          <TextField
            label="Белки"
            type="number"
            sx={{ maxWidth: 120 }}
            value={proteins.value}
            onChange={proteins.onchange}
            error={proteins.error !== null}
            helperText={proteins.error}
          />
          <TextField
            label="Жиры"
            type="number"
            sx={{ maxWidth: 120 }}
            value={fats.value}
            onChange={fats.onchange}
            error={fats.error !== null}
            helperText={fats.error}
          />
          <TextField
            label="Углеводы"
            type="number"
            sx={{ maxWidth: 120 }}
            value={carbo.value}
            onChange={carbo.onchange}
            error={carbo.error !== null}
            helperText={carbo.error}
          />
          <UploadButton
            fileName={file.value && file.value.name}
            onChange={file.onchange}
            reset={file.reset}
          >
            Файл
          </UploadButton>
        </Stack>
      </form>
      <FullFeaturedCrudGrid />
    </AdminLayout>
  );
};

export default ProductPage;
