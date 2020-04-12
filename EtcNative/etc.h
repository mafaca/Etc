#ifndef ETC_H
#define ETC_H

#include <stdint.h>

void decode_etc1(const void*, const int, const int, uint32_t*);
void decode_etc2(const void*, const int, const int, uint32_t*);
void decode_etc2a1(const void*, const int, const int, uint32_t*);
void decode_etc2a8(const void*, const int, const int, uint32_t*);
void decode_eac_r(const void*, const int, const int, uint32_t*);
void decode_eac_r_sign(const void*, const int, const int, uint32_t*);
void decode_eac_rg(const void*, const int, const int, uint32_t*);
void decode_eac_rg_sign(const void*, const int, const int, uint32_t*);

#endif /* end of include guard: ETC_H */