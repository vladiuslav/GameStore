const fetchUserGetAll = async () => {
  const result = await fetch("https://localhost:7025/api/User", {
    method: "GET",
    headers: {
      "Access-Control-Allow-Origin": "*",
      Accept: "application/json, text/plain, */*",
      "Content-Type": "application/json;charset=utf-8",
    },
  });
  return result;
};
export default fetchUserGetAll;
