 --PADר����Ʒ��Ϣ�ű�
 Create Or Replace View vSkuForPad as
 Select A.OrgCode,A.DepId,A.DepCode,A.DepName,C.ShpId,C.ShpCode,C.ShpName, --��֯����ר����Ϣ
       A.PluId,A.PluCode,A.PluName,'*' as ExPluCode,A.BarCode,A.Unit,A.Spec,A.Price,A.HyPrice --��Ʒ��Ϣ    
 From tSkuGtPlu A,tSkuPlu B,tSkuShopPlu C
Where A.PluId=B.PluId And A.PluId=C.PluId
  And A.OrgCode=C.OrgCode And A.DepId=C.DepId
  And A.PosComUpType<>'2' And C.PosComUpType<>'2'
Order By A.OrgCode,A.PluID Desc
