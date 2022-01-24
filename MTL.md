# Ork Flavoured MTL
OrkEngine does not support the standard MTL files, instead it supports OFM files(dont worry, it can still open .mtl files).  
These files only have a few notable differences, but the main one is that
***There is only one material/mesh***   
That's right! That is the biggest difference. So, the expected MTL file should look something like:^
```mtl
newmtl mainmaterial
Ns 225.000000
Ka 1.000000 1.000000 1.000000
Kd 0.800000 0.800000 0.800000
Ks 0.500000 0.500000 0.500000
Tx path
```properties. All MTL files *should* still load, but may not load properly.
It also adds the Tx parameter which represents a texture.
